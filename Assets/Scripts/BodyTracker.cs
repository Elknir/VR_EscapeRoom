using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.XR;


public class BodyTracker : MonoBehaviour
{
    public Camera head;
    void Awake()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        // Chopper la pos du casque via system input
        head = Camera.main;
        this.transform.position = new Vector3(head.transform.position.x, head.transform.position.y - 0.5f, head.transform.position.z);
        Debug.Log(head.transform.localRotation.y * Mathf.Rad2Deg *2 );
        this.transform.rotation = Quaternion.Euler(0, head.transform.rotation.y * Mathf.Rad2Deg *2,0);
// l'appliquer en perma sur le body avec un offset


    }



  
}
