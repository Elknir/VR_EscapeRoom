using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;
using System.Linq;


public class TaquinEnigmaManager : Enigma
{
    private GameObject[] AllTaquins;


    private bool heldTaquin;
    
    private void Start()
    {
        //SET TOUT LES TAQUINS AVEC LA BONNE DIRECTION
        //DONC LE FAIRE MEME APRES DE PLACEMENT
        //FAIRE UN CHECK DES COORDONNES DES TAQUINS POUR LA FIN
        
        var objects = FindObjectsByType(typeof(TaquinTile), FindObjectsSortMode.None);
        
        Array.Sort(objects, (a,b) => a.name.CompareTo(b.name));
        for (int i = 0; i < objects.Length; i++)
        {
            Object element = objects[i];
            Debug.Log(element);

            Debug.Log(i % 3); 
        }
    }


    public void TileGrabbed(Action validateGrab)
    {
        if (!heldTaquin)
        {
            heldTaquin = true;
            validateGrab();
        }
    }
    public void TileDropped(Action validateGrab)
    {
        heldTaquin = false;
        validateGrab();
    }


    protected override bool EnigmaCondition()
    {
        return false;
    }

    public override void ValidEnigma()
    {
        
    }
}