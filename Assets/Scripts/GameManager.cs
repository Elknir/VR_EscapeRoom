using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager : MonoBehaviour
{
    
    //TODO : faire le menu de cheat pour valider les enigmes dès le début
    
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
                        candle.GetComponent<XRGrabInteractable>().enabled = false;
                        candle.GetComponent<Rigidbody>().isKinematic = true;

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
