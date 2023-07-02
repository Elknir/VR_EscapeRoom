using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.Events;
using Matrix4x4 = UnityEngine.Matrix4x4;

public class TaquinTile : XRBaseInteractable
{
    [SerializeField]
    private DirectionEnum movingDirection;

    private Vector3 lockedPosition;
    
    public UnityEvent validPlacement;

    private Renderer myRenderer;
    private bool isHolding = false;
    private GameObject targetHand;
    
    protected override void Awake()
    {
        base.Awake();
        lockedPosition = transform.position;

        myRenderer = GetComponent<Renderer>();
    }

    public void HoldItem()
    {
        isHolding = !isHolding;

        if (!isHolding)
        {
            if (lockedPosition.y - transform.position.y  > Math.Abs(myRenderer.bounds.size.y / 2) )
            {
                LockTile();
            }
            else
            {
                  transform.position= lockedPosition;
            }
        }
    }

    private void IsHandToFar()
    {
        if (Vector3.Distance(transform.position, targetHand.transform.position) > 1.45)
        {
            HoldItem();
        }
    }


    private void Update()
    {
        if(movingDirection == DirectionEnum.None || !isHolding) return;
        
        IsHandToFar();

        Vector3 HandPosition = targetHand.transform.position;
        switch (movingDirection)
        {
            case DirectionEnum.Down:
                if (Convert(HandPosition).y < lockedPosition.y)
                {
                    if (Convert(HandPosition).y > lockedPosition.y - Math.Abs(myRenderer.bounds.size.y))
                    {
                        transform.position = new Vector3(lockedPosition.x, Convert(HandPosition).y,lockedPosition.z);
                    }
                }
                break;
            case DirectionEnum.Right:
                if (HandPosition.z > lockedPosition.z)
                {
                    transform.position = new Vector3(lockedPosition.x, lockedPosition.y,HandPosition.z);
                }
                break;
            case DirectionEnum.Left:
                if (HandPosition.z < lockedPosition.z)
                {
                    transform.position = new Vector3(lockedPosition.x, lockedPosition.y,HandPosition.z);
                }
                break;
            case DirectionEnum.Up:
                if (HandPosition.y > lockedPosition.y)
                {
                    transform.position = new Vector3(lockedPosition.x, HandPosition.y,lockedPosition.z);
                }
                break;

            default:
                break;
        }
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
        targetHand = args.interactor.gameObject;
    }

    private void LockTile()
    {
        transform.position = new Vector3(lockedPosition.x,lockedPosition.y  - Math.Abs(myRenderer.bounds.size.y) , lockedPosition.z); 
        lockedPosition = transform.position;
        
        validPlacement?.Invoke();
    }

    private Vector3 Convert(Vector3 targetVector)
    {
        return targetVector - myRenderer.bounds.center + transform.position;
    }
    
    public void OnDrawGizmosSelected()
    {
        if (myRenderer == null)
            return;
        var bounds = myRenderer.bounds;
        Gizmos.matrix = Matrix4x4.identity;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(bounds.center,0.01f);
        Gizmos.DrawWireCube(bounds.center, bounds.extents * 2);
    }

}
