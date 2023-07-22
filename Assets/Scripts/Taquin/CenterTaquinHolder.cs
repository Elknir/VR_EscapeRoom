using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class CenterTaquinHolder : MonoBehaviour
{
    [HideInInspector]
    public GameObject targetMiddleTaquin;
    private XRSocketInteractor socket;
    private bool itemFound = false;

    public UnityEvent validPlacement;
    
    private void Awake()
    {
        socket = GetComponent<XRSocketInteractor>();
    }
 
    //bougie arrivé au point d'attache
    private void MiddleTaquinPlaced()
    {
            validPlacement.Invoke();
    }

    private void Update()
    {
        
        if (itemFound)
        {
            if (targetMiddleTaquin.transform.position == socket.attachTransform.transform.position)
            {
                MiddleTaquinPlaced();
                itemFound = false;
            }
        }
    }

    //bougie laché dans un socket mais pas forcement arrivé
    public void MiddleTaquinFound()
    {
        itemFound = true;
        IXRSelectInteractable socketMiddleTaquin = socket.GetOldestInteractableSelected();
        targetMiddleTaquin = socketMiddleTaquin.transform.gameObject;
    }

   
}
