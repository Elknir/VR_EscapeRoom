using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.XR;


public class BodyTracker : MonoBehaviour
{
    public GameObject head;
    public GameObject book;

    private Vector3 bookPos, bookScale;
    private Quaternion bookRotation;
    private BookManager bookManager;
    void Awake()
    {
        if (!book)
        {
            Debug.LogError("No book assigned !");
            return;
        }
        
        bookPos = book.transform.localPosition;
        bookRotation =  Quaternion.Euler(book.transform.localEulerAngles) ;
        bookScale = book.transform.localScale;
        // Debug.Log($"pos : {bookPos} , rotation {bookRotation}, scale {bookScale}");
        bookManager = book.GetComponent<BookManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!head)
        {
            Debug.LogError("No head assigned !");
            return;
        }
        
        this.transform.position = new Vector3(head.transform.position.x, head.transform.position.y - 0.5f, head.transform.position.z);
        this.transform.rotation = Quaternion.Euler(0, head.transform.localEulerAngles.y ,0);
    }

    public void ResetBookPos()
    {
        // fermer le livre
        if (!book) return;
        
        
        book.transform.localPosition = bookPos;
        book.transform.localRotation = bookRotation;
        book.transform.localScale = bookScale;
        
    }

  
}
