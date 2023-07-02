using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaquinEnigmaManager : Enigma
{
    private GameObject[] AllTaquins;
    public int width = 3, height = 3;

    public GameObject taquins;
    
    private void Start()
    {

        if (!taquins)
        {
            Debug.LogError("Please set an attach for the taquin !");
            return;
        }

        ;
    
        // Debug.Log(taquins.transform.GetChild(0).);
        


        int childCount = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (i == 1 && j == 1)
                {
                    continue;
                }

                
                GameObject targetTaquin = taquins.transform.GetChild(childCount).gameObject; 
                childCount++;
            }
        }
    }


    protected override bool EnigmaCondition()
    {
        return false;
    }

    public override void ValidEnigma()
    {
        
    }
}
