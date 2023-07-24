using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class BolBehaviour : MonoBehaviour
{

    public Ingredients[] validIngredients;
    private List<Ingredients> ingredientsInPotion = new List<Ingredients>();
    private bool isNeedingBlood = false, isFinished = false;
    public UnityEvent validPlacement;


    // private ParticleSystem particleSystem;

    private void Start()
    {
        // particleSystem = transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {

        if (isNeedingBlood)
        {
            if (other.tag == "DaggerDetector" )
            {
                BloodLogic();
            }
        }
        else
        {
            if (other.tag == "DaggerDetector" || isFinished)
            {
                return;
            }
        
            Ingredients targetIngedient = other.transform.parent.GetComponent<PotionBehaviour>().ingredient;
            if (!isNeedingBlood)
            {
                BaseLogic(targetIngedient);   
            }
        }
    }


    private void BaseLogic(Ingredients targetIngedient)
    {
        if (validIngredients.Contains(targetIngedient))
        {
            // Ajouter l ingedient
            if (!ingredientsInPotion.Contains(targetIngedient))
            {
                ingredientsInPotion.Add(targetIngedient);
                    
                if (ingredientsInPotion.Count == validIngredients.Length)
                {
                    isNeedingBlood = true;
                }
            }
        }
        else
        {
            //Faire un reset
            ingredientsInPotion.Clear();
        }
    }


    private void BloodLogic()
    {
        validPlacement.Invoke();
    }
}
