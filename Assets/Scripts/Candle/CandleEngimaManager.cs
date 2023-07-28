using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using Unity.VisualScripting;
using UnityEditor;


public class CandleEngimaManager : Enigma 
{
    //TODO : faire un bouton GUI pour dire le nombre de candleHolder présent dans la scène
    public Object[] allCandleHolders;
    public int totalCandlesHolders;
    private int currentValidCandles = 0;


    public void ValidCandlePlaced()
    {
        currentValidCandles++;
        CheckEnigmaValidation(currentEnigma);
    }

    public void RemoveValidCandle()
    {
        currentValidCandles--;
    }

    protected override bool EnigmaCondition()
    {
        return currentValidCandles == totalCandlesHolders ?  true :  false;
    }

    public override void ValidEnigma()
    {
        //Ouvrir une trape pour faire apparaitre la boule de cristal

        
        //Chopper tout les sockets et bloquer les bougies à l'interieur 
        if (allCandleHolders.Length == 0) return;

        foreach (var element in allCandleHolders)
        {
            Debug.Log("Candle locked !");
            element.GetComponent<XRSocketInteractor>().enabled = false;
            var candle = element.GetComponent<CandleHolder>().targetCandle;
            if (candle)
            {
                candle.GetComponent<XRGrabInteractable>().enabled = false;
                candle.GetComponent<Rigidbody>().isKinematic = true;
            }
        }

        FMODUnity.RuntimeManager.PlayOneShot("event:/Manipulation/Altar/Altar/Man_Alt_Completed");
    }
    
    public void FindAllCandlesHolders()
    {
        allCandleHolders = FindObjectsByType(typeof(CandleHolder), FindObjectsSortMode.None);
        totalCandlesHolders = allCandleHolders.Length;
    }
    
#if UNITY_EDITOR
    [CustomEditor(typeof(CandleEngimaManager))]
    public class CandleEngimaEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            CandleEngimaManager container = (CandleEngimaManager)target;
            EditorGUI.BeginDisabledGroup(Application.isPlaying);
            if(GUILayout.Button("Find all candle holders"))
            {
                container.FindAllCandlesHolders();
            }
            EditorGUI.EndDisabledGroup();
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
