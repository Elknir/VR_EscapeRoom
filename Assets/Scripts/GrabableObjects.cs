using UnityEngine;

public class GrabableObjects : MonoBehaviour
{
    protected bool isHeld = false;
    
    public virtual void HoldItem()
    {
        isHeld = true;
    }
    
    public virtual void DropItem()
    {
        isHeld = false;
    }

    public virtual void UseItem()
    {
        
    }
}
