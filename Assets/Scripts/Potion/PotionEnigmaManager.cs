using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionEnigmaManager : Enigma
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
        //Effets + changer la couleur de la potion?
        //isFinished
        
        var container =FindObjectsOfType<ContainerBehaviour>();;
        container[0].isFinised = true;
    }
}
