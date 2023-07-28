using System.Threading;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Candle : GrabableObjects
{
    //TODO : faire que le mesh s'adapte quand on change les properties
    public CandleProperties properties;

    public void LockInPlace()
    {
        Debug.Log("Locked !");
        
    }
}