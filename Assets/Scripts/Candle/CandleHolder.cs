using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class CandleHolder : MonoBehaviour
{
    public CandleProperties properties;
    [HideInInspector]
    public GameObject targetCandle;
    private XRSocketInteractor socket;
    private bool itemFound = false;

    public UnityEvent validPlacement;
    public UnityEvent removePlacement;
    
    private void Awake()
    {
        socket = GetComponent<XRSocketInteractor>();
        
    }
 
    //bougie arrivé au point d'attache
    private void CandlePlaced()
    {
        
        Candle targetCandleScript = targetCandle.GetComponent<Candle>();
        
        if (!targetCandleScript)
        {
            Debug.LogError("No candleType script assigned to the socketed candle");
        }
        
        CandleProperties candleProperties = targetCandleScript.properties;
        if (properties.Match(candleProperties))
        {
            validPlacement.Invoke();
        }
    }

    private void Update()
    {
        
        if (itemFound)
        {
            if (targetCandle.transform.position == socket.attachTransform.transform.position)
            {
                CandlePlaced();
                itemFound = false;
            }
        }
    }

    //bougie laché dans un socket mais pas forcement arrivé
    public void CandleFound()
    {
        itemFound = true;
        IXRSelectInteractable socketCandle = socket.GetOldestInteractableSelected();
        targetCandle = socketCandle.transform.gameObject;
    }

    public void CandleRemoved()
    {
        if(!socket.enabled) return;
        itemFound = false;
        
        Candle targetCandleScript = targetCandle.GetComponent<Candle>();
        
        CandleProperties candleProperties = targetCandleScript.properties;
        if (properties.Match(candleProperties))
        {
            removePlacement.Invoke();
        }
    }
}


