using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionOnBodyEnigmaManager : Enigma
{
    protected override bool EnigmaCondition()
    {
        return true;
    }

    public void ReceiveSignal()
    {
        CheckEnigmaValidation(currentEnigma);
    }

    public override void ValidEnigma()
    {
        //Vider la potion?
        //Effets sur anna
    }
}
