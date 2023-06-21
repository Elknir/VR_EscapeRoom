using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public bool cheatTaquin, cheatPotion, cheatCandles, cheatPowder, cheatDance;

    public void Start()
    {
        if (cheatTaquin) ReciveSignal(EnigmaEnum.Taquin);
        if(cheatPotion) ReciveSignal(EnigmaEnum.Potion);
        if(cheatCandles) ReciveSignal(EnigmaEnum.Candles);
        if(cheatPowder) ReciveSignal(EnigmaEnum.Powder);
        if(cheatDance) ReciveSignal(EnigmaEnum.Dance);
    }
    
    public void ReciveSignal(EnigmaEnum targetEngima)
    {
        switch(targetEngima) 
        {
            case EnigmaEnum.Taquin:
                Debug.Log("GG ! T'as fini l'épreuve du taquin !");
                //Ouvrir le tiroir de la dague sacrificielle
                //Posiblement faire apparaitre les objets
                break;
            case EnigmaEnum.Potion:
                Debug.Log("GG ! T'as fini l'épreuve des potions !");
                //Doit être validé une fois que le joueur a mis la potion de vie sur la fille
                //Faire un truc spécifique?
                break;
            case EnigmaEnum.Candles:
                Debug.Log("GG ! T'as fini l'épreuve des bougies !");
                //Chopper tout les sockets et bloquer les bougies à l'interieur 
                var candleHolders = FindObjectsByType(typeof(CandleHolder), FindObjectsSortMode.None);
                foreach (var element in candleHolders)
                {
                    element.GetComponent<XRSocketInteractor>().enabled = false;
                    var candle = element.GetComponent<CandleHolder>().targetCandle;
                    if (candle)
                    {
                        candle.GetComponent<XRGrabInteractable>().enabled = false;
                        candle.GetComponent<Rigidbody>().isKinematic = true;
                    }
                }
                //Ouvrir une trape pour faire apparaitre la boule de cristal
                break;
            case EnigmaEnum.Powder:
                Debug.Log("GG ! T'as fini l'épreuve de la poudre !");
                //Si potion de vie finie faire apparaitre la page du grimoire
                break;
            case EnigmaEnum.Dance:
                Debug.Log("GG ! T'as fini l'épreuve de la dance !");
                //Bruler le Grimoire
                //Faire Apparaitre le démon
                //Puis fin du jeu?
                break;

        }
    }
}

[CustomEditor(typeof(GameManager))]
public class EnemyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GameManager container = (GameManager)target;
        base.OnInspectorGUI();

        EditorGUI.BeginDisabledGroup(Application.isPlaying);
        container.cheatDance = EditorGUILayout.Toggle("Cheat Dance", container.cheatDance);
        container.cheatPowder = EditorGUILayout.Toggle("Cheat Powder", container.cheatPowder);
        container.cheatCandles = EditorGUILayout.Toggle("Cheat Candles", container.cheatCandles);
        container.cheatPotion = EditorGUILayout.Toggle("Cheat Potion", container.cheatPotion);
        container.cheatTaquin = EditorGUILayout.Toggle("Cheat Taquin", container.cheatTaquin);
        
        if (container.cheatDance || container.cheatPowder || container.cheatCandles || container.cheatPotion ||
            container.cheatTaquin)
        {
            if(GUILayout.Button("Disable all cheats"))
            {
                enableCheat(false, container);
            }
        }
        else
        {
            if(GUILayout.Button("Enable all cheats"))
            {
                enableCheat(true, container);

            }
        }
        EditorGUI.EndDisabledGroup();
        serializedObject.ApplyModifiedProperties();
    }


    public void enableCheat(bool enable, GameManager container)
    {
        container.cheatDance = enable;
        container.cheatPowder = enable;
        container.cheatCandles = enable;
        container.cheatPotion = enable;
        container.cheatTaquin = enable;
    }
}
