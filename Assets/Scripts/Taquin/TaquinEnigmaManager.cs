using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaquinEnigmaManager : Enigma
{
    private GameObject[] AllTaquins;


    private bool heldTaquin;
    
    private void Start()
    {
        //SET TOUT LES TAQUINS AVEC LA BONNE DIRECTION
        //DONC LE FAIRE MEME APRES DE PLACEMENT
        //FAIRE UN CHECK DES COORDONNES DES TAQUINS POUR LA FIN
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
