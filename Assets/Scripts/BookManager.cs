using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    
    public GameObject[] pages;
    // public float speedTurn;
    public int startingPage = 0;
    
    private int currentPage;
    private AnimState currentAnimState = AnimState.CloseStart;
        
    enum AnimState
    {
        CloseStart,
        OpenStart,
        Middle,
        OpenEnd,
        CloseEnd,
    };

    enum Sides
    {
        Left,
        Right,
        None,
    };

    private void Awake()
    {
        currentPage = startingPage;
        UpdatePage(Sides.None);
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
                if (currentAnimState != AnimState.CloseStart)
                {
                    currentPage--;
                }

                break;
            
            case Sides.Right:
                if (currentAnimState != AnimState.CloseEnd)
                {
                    currentPage++;
                }
                break;
            
            case Sides.None:
                break;
        }
        
        
        // TODO : search for switch alternative 
        if (currentPage == 0)
        {
            currentAnimState = AnimState.CloseStart;
        }
        else if (currentPage == 1)
        {
            currentAnimState = AnimState.OpenStart;
        }
        else if (currentPage == pages.Length - 1)
        {
            currentAnimState = AnimState.OpenEnd;
        }
        else if(currentPage == pages.Length)
        {
            currentAnimState = AnimState.CloseEnd;
        }
        else
        {
            currentAnimState = AnimState.Middle;
        }
        
        DebugPage();
    }

    void DebugPage()
    {
        Debug.Log(currentPage);
        Debug.Log(currentAnimState);

    }
    
}