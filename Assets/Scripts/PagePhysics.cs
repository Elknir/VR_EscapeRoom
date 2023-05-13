using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;



public class PagePhysics : MonoBehaviour
{
    private Rigidbody pageRigidbody;
    private HingeJoint turningHinge;
    private bool isArrived;
    void Start()
    {
        turningHinge = GetComponent<HingeJoint>();
        pageRigidbody = GetComponent<Rigidbody>();
        isArrived = false;
        EnableMovement(false);
        SetTargetVelocity(0);
    }


    public void Update()
    {
        if (!isArrived)
        {
            HasArrived();
        }
        
        if (isArrived && turningHinge.useMotor)
        {
            EnableMovement(false);
        }
        

    }


    public void TurnPage(BookManager.Sides side , float openSpeed)
    {
        
        
        switch (side)
        {
            case BookManager.Sides.Left:
                EnableMovement(true);
                SetTargetVelocity(-openSpeed);
                break;
            
            case BookManager.Sides.Right:
                EnableMovement(true);
                SetTargetVelocity(openSpeed);
                break;
        }

    }

    private void SetTargetVelocity(float velocity)
    {
        JointMotor myMotor = turningHinge.motor;
        myMotor.targetVelocity = velocity;
        turningHinge.motor = myMotor;
    }

    private void EnableMovement(bool activation)
    {
        isArrived = false;
        pageRigidbody.isKinematic = !activation;
        turningHinge.useMotor = activation;
    }


    private void HasArrived()
    {
        if (turningHinge.motor.targetVelocity > 0)
        {
            if (turningHinge.angle >= turningHinge.limits.max - 1)
            {
                isArrived = true;
            }
        }
        else if (turningHinge.motor.targetVelocity < 0)
        {
            if (turningHinge.angle <= turningHinge.limits.min + 1)
            {
                isArrived = true;
            }
        }
    }

}
