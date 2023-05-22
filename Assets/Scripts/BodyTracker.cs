using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.XR;


public class BodyTracker : MonoBehaviour
{
    public GameObject head;
    // public Transform BookAttachPos;
    // public GameObject Book;
    void Awake()
    {
        // this.Book.transform.position = BookAttachPos.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Chopper la pos du casque via system input
        this.transform.position = new Vector3(head.transform.position.x, head.transform.position.y - 0.5f, head.transform.position.z);
        
        this.transform.rotation = Quaternion.Euler(0, head.transform.localEulerAngles.y ,0);

        
    }



  
}
