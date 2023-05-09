using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    
    public GameObject[] pages;
    // public float speedTurn;
    public int startingPage = 0;
    
    private int currentPage;
    private BookState currentBookState = BookState.CloseStart;

    [Header("Book Meshes")] 
    public GameObject bookCloseLeft;
    public GameObject bookOpenLeft;
    public GameObject bookMiddle;
    public GameObject bookOpenRight;
    public GameObject bookCloseRight;
    private GameObject[] books = new GameObject[5];

    

    enum BookState
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

        books[0] = bookCloseLeft;
        books[1] = bookOpenLeft;
        books[2] = bookMiddle;
        books[3] = bookOpenRight;
        books[4] = bookCloseRight;

        int i = 0;
        foreach (var bookGo in books)
        {
            GameObject go = Instantiate(bookGo, transform.position, transform.rotation, transform) ;
            go.SetActive(false);
            books[i] = go;
            i++;
        }

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
            
            case Sides.None:
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
        
        // DebugPage();
    }

    void SetBookState(BookState newBookState)
    {
        if(currentBookState == newBookState) return;
        
        books[(int)currentBookState].SetActive(false);
        currentBookState = newBookState;
        books[(int)currentBookState].SetActive(true);
    }

    void DebugPage()
    {
        Debug.Log($"Current page : {currentPage} , {currentBookState}" );
    }
    
}