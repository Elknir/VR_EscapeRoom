using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    public GameObject cover;
    public GameObject[] pages;
    // public float speedTurn;
    public int startingPage = 0;
    public float offsetBetweenPages = 0.1f;
    
    private int currentPage;
    private BookState currentBookState = BookState.CloseStart;



    

    enum BookState
    {
        CloseStart,
        OpenStart,
        Middle,
        OpenEnd,
        CloseEnd,
    };

    public enum Sides
    {
        Left,
        Right,
    };
    
    

    private void Awake()
    {
        currentPage = startingPage;

        float i = 1;
        foreach (GameObject page in pages)
        {
            page.transform.localPosition = new Vector3(0, page.transform.localPosition.y + offsetBetweenPages * i , 0);
            i++;
        }
        cover.transform.localPosition = new Vector3(0, cover.transform.localPosition.y + offsetBetweenPages * i, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            UpdatePage(Sides.Left);
        }
        
        if (Input.GetKeyDown(KeyCode.M))
        {
            UpdatePage(Sides.Right);
        }
    }

    void UpdatePage(Sides turnSide)
    {
        //check a faire
        switch (turnSide)
        {
            case Sides.Left:
                if (currentBookState != BookState.CloseStart)
                {
                    currentPage--;
                }

                break;
            
            case Sides.Right:
                if (currentBookState != BookState.CloseEnd)
                {
                    currentPage++;
                }
                break;
            
   
        }
        
        
        // TODO : search for switch alternative 
        if (currentPage == 0)
        {
            SetBookState(BookState.CloseStart);
        
        }
        else if (currentPage == 1)
        {
            SetBookState(BookState.OpenStart);
        
        }
        else if (currentPage == pages.Length - 1)
        {
            SetBookState(BookState.OpenEnd);
        
        }
        else if(currentPage == pages.Length)
        {
            SetBookState(BookState.CloseEnd);
        }
        else
        {
            SetBookState(BookState.Middle);
        }
        
    }

    void SetBookState(BookState newBookState)
    {
        // if(currentBookState == newBookState) return;
        currentBookState = newBookState;
    }

    void DebugPage()
    {
        Debug.Log($"Current page : {currentPage} , {currentBookState}" );
    }
    
}