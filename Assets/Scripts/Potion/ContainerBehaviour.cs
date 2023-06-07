using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerBehaviour : PotionBehaviour
{
    [Range(-1, 1)]public float offset;

    private Collider colliderTop;
    private Vector3 collisionPoint;
    

    protected override void Awake()
    {
        base.Awake();
        colliderTop = top.GetComponent<Collider>();
        
    }

    protected override void PotionLogic()
    {
        bot.transform.position = transform.position - new Vector3(0,offset,0);
        collisionPoint = colliderTop.ClosestPoint(bot.transform.position);
        potionParticleObject.transform.position = collisionPoint;
        
        // TODO : faire un offset
        // potionParticleObject.transform.localPosition -= new Vector3(0, 0.05f, 0);

        var distanceFromCenter = Vector3.Distance(transform.position, collisionPoint);
        
        if (distanceFromCenter >= 0.09 && !isActive)
        {
            ParticleEnable(true);

        }
        else if(distanceFromCenter <= 0.09 && isActive)
        {
            ParticleEnable(false);

        }
    }
    
    
    void OnDrawGizmos()
    {
        if (isHeld)
        {
            // DEBUG
            // Gizmos.color = Color.red;
            // Gizmos.DrawSphere(collisionPoint, 0.05f);
        }
    }
}
