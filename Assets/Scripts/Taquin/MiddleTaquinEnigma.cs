using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MiddleTaquinEnigma : Enigma
{
    protected override bool EnigmaCondition()
    {
        return true;
    }

    public override void ValidEnigma()
    {
        var centerTaquinHolder =FindObjectsOfType<CenterTaquinHolder>();;
        Debug.Log("Middle Taquin locked !");
        centerTaquinHolder[0].gameObject.GetComponent<XRSocketInteractor>().socketActive = true;
        var targetMiddleTaquin = centerTaquinHolder[0].gameObject.GetComponent<CenterTaquinHolder>().targetMiddleTaquin;
        targetMiddleTaquin.GetComponent<XRGrabInteractable>().enabled = false;
        targetMiddleTaquin.GetComponent<Rigidbody>().isKinematic = true;
    }

    // Update is called once per frame
    public void ValidMiddleTaquinPlaced()
    {
        CheckEnigmaValidation(currentEnigma);
    }
}