using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CenterPiece : GrabableObjects
{
    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);

        FMODUnity.RuntimeManager.PlayOneShot("event:/Manipulation/Taquin/Square/Man_Squ_PickUp", gameObject.transform.position);
    }
}
