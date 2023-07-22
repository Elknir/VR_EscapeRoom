using System;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.Events;
using Matrix4x4 = UnityEngine.Matrix4x4;
using Vector2 = UnityEngine.Vector2;
using UnityEditor;


public class TaquinEvent : UnityEvent<Action>
{
}

public class TaquinPlacedEvent : UnityEvent<Vector2>
{
}

public class TaquinTile : XRBaseInteractable
{

    // [SerializeField]
    [HideInInspector]
    public DirectionEnum movingDirection;

    [HideInInspector]
    public Vector3 lockedPosition;
    
    private Renderer myRenderer;
    private bool isHolding = false;
    private GameObject targetHand;

    [HideInInspector]
    public TaquinPlacedEvent validPlacement;
    [HideInInspector]
    public TaquinEvent tileGrabbed, tileDropped ;
    private Action validateGrab;
    
    [HideInInspector]
    public Vector2 coordinates;
    
    //POUR LE CHECKING FAUT : UN GOAL COORDS
    [HideInInspector]
    public Vector2 goalCoords;
    
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
            validPlacement = new TaquinPlacedEvent();
        }
        tileGrabbed.AddListener(taquinEnigmaManager.TileGrabbed);
        tileDropped.AddListener(taquinEnigmaManager.TileDropped);
        validPlacement.AddListener(taquinEnigmaManager.ValidTaquinPlaced);

    }
    
    public void HoldItem()
    {
        isHolding = true;
    }

    public void DropItem()
    {
        isHolding = false;
        
        
        switch (movingDirection)
        {
            case DirectionEnum.Down:
                if (lockedPosition.y - transform.position.y  > Math.Abs(myRenderer.bounds.size.y / 2) )
                {
                    LockTile();
                }
                else
                {
                    transform.position = lockedPosition;
                }
                break;
            case DirectionEnum.Right:
                if ( transform.position.z - lockedPosition.z > Math.Abs(myRenderer.bounds.size.z / 2) )
                {
                    LockTile();
                }
                else
                {
                    transform.position = lockedPosition;
                }
                break;
            case DirectionEnum.Left:
                if (lockedPosition.z - transform.position.z> Math.Abs(myRenderer.bounds.size.z / 2) )
                {
                    LockTile();
                }
                else
                {
                    transform.position = lockedPosition;
                }
                break;
            case DirectionEnum.Up:
                if ( transform.position.y - lockedPosition.y > Math.Abs(myRenderer.bounds.size.y / 2) )
                {
                    LockTile();
                }
                else
                {
                    transform.position = lockedPosition;
                }
                break;
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
                        // N'EST PAS MATHEMATIQUEMENT CORRECT = LEGER DECALAGE         
                        transform.localPosition += new Vector3((transform.position.y - lockedPosition.y)/10, 0, 0);
                    }
                }
                break;
            case DirectionEnum.Right:
                if (Convert(HandPosition).z > lockedPosition.z)
                {
                    if (Convert(HandPosition).z < lockedPosition.z + Math.Abs(myRenderer.bounds.size.z))
                    {
                        transform.position = new Vector3(lockedPosition.x,lockedPosition.y,Convert(HandPosition).z);
                    }
                }
                break;
            case DirectionEnum.Left:
                if (Convert(HandPosition).z < lockedPosition.z)
                {
                    if (Convert(HandPosition).z > lockedPosition.z - Math.Abs(myRenderer.bounds.size.z))
                    {
                        transform.position = new Vector3(lockedPosition.x,lockedPosition.y,Convert(HandPosition).z);
                    }
                }
                break;
            case DirectionEnum.Up:
                if (Convert(HandPosition).y > lockedPosition.y)
                {
                    if (Convert(HandPosition).y < lockedPosition.y + Math.Abs(myRenderer.bounds.size.y))
                    {
                        transform.position = new Vector3(lockedPosition.x, Convert(HandPosition).y,lockedPosition.z);
                        // N'EST PAS MATHEMATIQUEMENT CORRECT = LEGER DECALAGE         
                        transform.localPosition += new Vector3((transform.position.y - lockedPosition.y)/10, 0, 0);
                    }
                }
                break;
        }
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
        targetHand = args.interactorObject.transform.gameObject;
        tileGrabbed.Invoke(() => {HoldItem();});
        
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);
        tileDropped.Invoke(() => {DropItem();});
    }

    private void LockTile()
    {
        // N'EST PAS MATHEMATIQUEMENT CORRECT = LEGER DECALAGE
        switch (movingDirection)
        {
            case DirectionEnum.Down:
                transform.position = new Vector3(lockedPosition.x + Math.Abs(myRenderer.bounds.size.y) / 10,lockedPosition.y  - Math.Abs(myRenderer.bounds.size.y) , lockedPosition.z); 
                break;
            case DirectionEnum.Right:
                transform.position = new Vector3(lockedPosition.x ,lockedPosition.y , lockedPosition.z + Math.Abs(myRenderer.bounds.size.z)); 
                break;
            case DirectionEnum.Left:
                transform.position = new Vector3(lockedPosition.x ,lockedPosition.y , lockedPosition.z - Math.Abs(myRenderer.bounds.size.z)); 
                break;
            case DirectionEnum.Up:
                transform.position = new Vector3(lockedPosition.x - Math.Abs(myRenderer.bounds.size.y) / 10,lockedPosition.y + Math.Abs(myRenderer.bounds.size.y) , lockedPosition.z); 
                break;
        }


        //Envoyer le signal au manager
        //Et prendre la positon qu'il avait avant pour envoyer les nouvelles directions aux taquins a coté
        validPlacement?.Invoke(coordinates);
        
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

    
#if UNITY_EDITOR
    [CustomEditor(typeof(TaquinTile))]
    public class CheatEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            TaquinTile container = (TaquinTile)target;
            base.OnInspectorGUI();

            // EditorGUI.BeginDisabledGroup(Application.isEditor);
            if (container.movingDirection != DirectionEnum.None)
            {
                if(GUILayout.Button("Lock tile"))
                {
                    container.LockTile();
                }
            }
            // EditorGUI.EndDisabledGroup();
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
