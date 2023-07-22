using System;
using UnityEngine;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public bool cheatTaquin, cheatPotion, cheatCandles, cheatTaquinMiddle, cheatPotionOnBody;
    private bool validateTaquin, validatePotion, validateCandles, validateTaquinMiddle, validatePotionOnBody;

    public void Start()
    {
        if (cheatTaquin) EmitSignal(EnigmaEnum.Taquin);
        if(cheatPotion) EmitSignal(EnigmaEnum.Potion);
        if(cheatCandles) EmitSignal(EnigmaEnum.Candles);
        if(cheatTaquinMiddle) EmitSignal(EnigmaEnum.TaquinMiddle);
        if(cheatPotionOnBody) EmitSignal(EnigmaEnum.PotionOnBody);
    }
    
    public void ReciveSignal(EnigmaEnum targetEngima, Action validateCallBack)
    {
        switch(targetEngima) 
        {
            case EnigmaEnum.Taquin:
                Debug.Log("GG ! T'as fini l'épreuve du taquin !");
                //Ouvrir le tiroir de la dague sacrificielle
                validateTaquin = true;
                validateCallBack();
                //Posiblement faire apparaitre les objets
                break;
            case EnigmaEnum.Potion:
                Debug.Log("GG ! T'as fini l'épreuve des potions !");
                //Doit être validé une fois que le joueur a mis la potion de vie sur la fille
                validatePotion = true;
                validateCallBack();
                //Faire un truc spécifique?
                break;
            case EnigmaEnum.Candles:
                Debug.Log("GG ! T'as fini l'épreuve des bougies !");
                validateCandles = true;
                validateCallBack();
                break;
            case EnigmaEnum.TaquinMiddle:
                Debug.Log("GG ! T'as fini l'épreuve du TaquinMiddle !");
                //Si potion de vie finie faire apparaitre la page du grimoire
                validateTaquinMiddle = true;
                validateCallBack();
                break;
            case EnigmaEnum.PotionOnBody:
                Debug.Log("GG ! T'as fini l'épreuve de la dance !");
                if (validateTaquin && validatePotion && validateCandles && validateTaquinMiddle)
                {
                    validatePotionOnBody = true;
                    validateCallBack();
                }
                //Bruler le Grimoire
                //Faire Apparaitre le démon
                //Puis fin du jeu?
                break;
        }
    }


    private void EmitSignal(EnigmaEnum targetEngima)
    {
        foreach (Transform child in transform)
        {
            Enigma comparedEnigma = child.GetComponent<Enigma>();
            if (comparedEnigma.currentEnigma== targetEngima)
            {
                comparedEnigma.ValidEnigma();
                return;
            }
        }
        Debug.LogWarning(targetEngima + " EngimaManager not found !" );
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(GameManager))]
public class CheatEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GameManager container = (GameManager)target;
        base.OnInspectorGUI();

        EditorGUI.BeginDisabledGroup(Application.isPlaying);
        container.cheatPotionOnBody = EditorGUILayout.Toggle("Cheat PotionOnBody", container.cheatPotionOnBody);
        container.cheatTaquinMiddle = EditorGUILayout.Toggle("Cheat TaquinMiddle", container.cheatTaquinMiddle);
        container.cheatCandles = EditorGUILayout.Toggle("Cheat Candles", container.cheatCandles);
        container.cheatPotion = EditorGUILayout.Toggle("Cheat Potion", container.cheatPotion);
        container.cheatTaquin = EditorGUILayout.Toggle("Cheat Taquin", container.cheatTaquin);
        
        if (container.cheatPotionOnBody || container.cheatTaquinMiddle || container.cheatCandles || container.cheatPotion ||
            container.cheatTaquin)
        {
            if(GUILayout.Button("Disable all cheats"))
            {
                EnableCheat(false, container);
            }
        }
        else
        {
            if(GUILayout.Button("Enable all cheats"))
            {
                EnableCheat(true, container);

            }
        }
        EditorGUI.EndDisabledGroup();
        serializedObject.ApplyModifiedProperties();
    }


    public void EnableCheat(bool enable, GameManager container)
    {
        container.cheatPotionOnBody = enable;
        container.cheatTaquinMiddle = enable;
        container.cheatCandles = enable;
        container.cheatPotion = enable;
        container.cheatTaquin = enable;
    }
}
#endif
