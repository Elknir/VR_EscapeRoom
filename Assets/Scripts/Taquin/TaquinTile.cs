using System;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.Events;
using Matrix4x4 = UnityEngine.Matrix4x4;

public class TaquinEvent : UnityEvent<Action>
{
}

public class TaquinTile : XRBaseInteractable
{
    [SerializeField]
    private DirectionEnum movingDirection;

    private Vector3 lockedPosition;
    

    private Renderer myRenderer;
    private bool isHolding = false;
    private GameObject targetHand;
    
    public UnityEvent validPlacement;
    public TaquinEvent tileGrabbed, tileDropped ;
    private Action validateGrab;

    protected override void Awake()
    {
        base.Awake();
        lockedPosition = transform.position;

        myRenderer = GetComponent<Renderer>();

        TaquinEnigmaManager taquinEnigmaManager =
            FindObjectsByType(typeof(TaquinEnigmaManager), FindObjectsSortMode.None)[0].GetComponent<TaquinEnigmaManager>();
        
        if (taquinEnigmaManager == null)
        {
            Debug.LogError("TAQUIN MANAGER MISSING ! ");

        }

        if (tileGrabbed == null || tileDropped == null)
        {
            tileGrabbed = new TaquinEvent();
            tileDropped = new TaquinEvent();
        }
        tileGrabbed.AddListener(taquinEnigmaManager.TileGrabbed);
        tileDropped.AddListener(taquinEnigmaManager.TileDropped);
    }
    
    public void HoldItem()
    {
        isHolding = true;
    }

    public void DropItem()
    {
        isHolding = false;
        
        if (lockedPosition.y - transform.position.y  > Math.Abs(myRenderer.bounds.size.y / 2) )
        {
            LockTile();
        }
        else
        {
            transform.position = lockedPosition;
        }
    }
            

   

    private bool IsHandToFar()
    {
        if (Vector3.Distance(transform.position, targetHand.transform.position) > 1.45)
        {
            tileDropped.Invoke(() => {DropItem();});
            return true;
        }
        return false;
    }

    private void Update()
    {
        
        //FAIRE TOUTES LES DIRECTIONS
        //FAIRE LA ROTATION EN X
        if(movingDirection == DirectionEnum.None || !isHolding || IsHandToFar()) return;

        Vector3 HandPosition = targetHand.transform.position;
        switch (movingDirection)
        {
            case DirectionEnum.Down:
                if (Convert(HandPosition).y < lockedPosition.y)
                {
                    if (Convert(HandPosition).y > lockedPosition.y - Math.Abs(myRenderer.bounds.size.y))
                    {
                        transform.position = new Vector3(lockedPosition.x, Convert(HandPosition).y,lockedPosition.z);
                        transform.localPosition += new Vector3((transform.position.y - lockedPosition.y)/10, 0, 0);
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
        tileGrabbed.Invoke(() => {HoldItem();});
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);
        tileDropped.Invoke(() => {DropItem();});
    }

    private void LockTile()
    {
        //POUR QUE CE SOIT POUR TOUTES LES DIRECTIONS
        transform.position = new Vector3(lockedPosition.x,lockedPosition.y  - Math.Abs(myRenderer.bounds.size.y) , lockedPosition.z); 

        //Envoyer le signal au manager
        //Et prendre la positon qu'il avait avant pour envoyer les nouvelles directions aux taquins a coté
        validPlacement?.Invoke();
        
        lockedPosition = transform.position;
        

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
