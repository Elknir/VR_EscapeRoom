using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnnaBody : MonoBehaviour
{
    public UnityEvent validPlacement;
    
    void OnParticleCollision(GameObject other)
    {
        Ingredients targetIngedient = other.transform.parent.GetComponent<PotionBehaviour>().ingredient;
        //chopper le tag du bol
        //return si pas bol
        
        //envoyer au gamemanager il se chargera lui meme de dire si c est valide
        if (targetIngedient == Ingredients.PotionFinale)
        {
            validPlacement.Invoke();
        }
    }
}
