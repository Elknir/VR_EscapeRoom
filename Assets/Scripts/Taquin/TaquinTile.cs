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
    private Vector3 lockedPosition, HandPosition;
    
    public UnityEvent validPlacement;

    private Renderer myRenderer;
    protected override void Awake()
    {
        
        base.Awake();
        lockedPosition = transform.position;
        myRenderer = GetComponent<Renderer>();
    }

    public void HoldItem()
    {

        // Debug.Log(myRenderer);
        
        switch (movingDirection)
        {
            case DirectionEnum.Down:
                Debug.Log(myRenderer.bounds.center.y - HandPosition.y);
                if (Convert(HandPosition).y < lockedPosition.y)
                {
                    transform.position = new Vector3(lockedPosition.x, Convert(HandPosition).y,lockedPosition.z);
                }
                // Convert()
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
            case DirectionEnum.None:
                break;
            default:
                break;
        }
        
        

    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
        HandPosition = args.interactor.gameObject.transform.position;
    }

    public void LockTile()
    {
        validPlacement?.Invoke();
        lockedPosition = transform.position;
    }

    //faut convertir tout ce qui est en lien avec la handPos

    private Vector3 Convert(Vector3 targetVector)
    {
        return targetVector -myRenderer.bounds.center + transform.position;
        
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
