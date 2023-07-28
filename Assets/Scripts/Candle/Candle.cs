using System.Threading;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Candle : GrabableObjects
{
    //TODO : faire que le mesh s'adapte quand on change les properties
    public CandleProperties properties;

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);

        FMODUnity.RuntimeManager.PlayOneShot("event:/Manipulation/Altar/Candle/Man_Can_Impact");
    }

    public void LockInPlace()
    {
        Debug.Log("Locked !");
    }
}