using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractibleObjects : XRBaseInteractable
{
    protected bool isHeld = false;
    [HideInInspector]
    public Material objectMaterial;

    private void Start()
    {
        objectMaterial = GetComponent<Renderer>().material;
    }
    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
        if(objectMaterial) objectMaterial.DisableKeyword("_EMISSION");
        isHeld = true;
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);
        if(objectMaterial ) objectMaterial.EnableKeyword("_EMISSION");
        isHeld = false;
    }

    protected override void OnHoverEntering(HoverEnterEventArgs args)
    {
        base.OnHoverEntering(args);
        if(objectMaterial ) objectMaterial.EnableKeyword("_EMISSION");
    }

    protected override void OnHoverExiting(XRBaseInteractor interactor)
    {
        base.OnHoverExiting(interactor);
        if(objectMaterial) objectMaterial.DisableKeyword("_EMISSION");
    }


    public virtual void UseItem(){}

}