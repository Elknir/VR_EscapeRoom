using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;



public class PagePhysics : MonoBehaviour
{
    
    private HingeJoint turningHinge;
    public int openSpeed = 100;
    void Start()
    {
        turningHinge = GetComponent<HingeJoint>();
        turningHinge.useMotor = true;
        SetTargetVelocity(0);
    }

    public void OpenBook(BookManager.Sides side)
    {
        
        switch (side)
        {
            case BookManager.Sides.Left:
                SetTargetVelocity(openSpeed);
                break;
            
            case BookManager.Sides.Right:
                SetTargetVelocity(-openSpeed);
                break;
        }

    }

    private void SetTargetVelocity(int velocity)
    {
        JointMotor myMotor = turningHinge.motor;
        myMotor.targetVelocity = velocity;
        turningHinge.motor = myMotor;
    }

    
    
}
