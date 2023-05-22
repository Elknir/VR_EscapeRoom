using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class BookManager : MonoBehaviour
{
    public GameObject cover;
    public GameObject[] pages;
    public float turnSpeed = 100;
    private int startingPage;
    
    private int currentPage;
    private BookState currentBookState = BookState.Start;

    private bool isHeld = false;
    
    
    
    enum BookState
    {
        Start,
        Middle,
        End,
    };

    public enum Sides
    {
        Left,
        Right,
    };
    
    private void Awake()
    {
        currentPage = startingPage;
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
        switch (turnSide)
        {
            case Sides.Left:
                
                if (currentBookState != BookState.Start)
                {
                    if (currentPage == 1)
                    {
                        cover.GetComponent<PagePhysics>().TurnPage(turnSide, turnSpeed);
                    
                    }
                    
                    if (currentPage > 1)
                    {
                        pages[currentPage - 2].GetComponent<PagePhysics>().TurnPage(turnSide, turnSpeed);
                    }
                    
                    currentPage--;
                }
                else
                {
                    if(!cover.GetComponent<PagePhysics>().isArrived) return;

                    
                    cover.GetComponent<PagePhysics>().TurnPage(Sides.Right, turnSpeed * 1.3f);

                    foreach (GameObject page in pages)
                    {
                        page.GetComponent<PagePhysics>().TurnPage(Sides.Right, turnSpeed * 1.3f);
                    }

                    currentPage = pages.Length+ 1;
                }

                
                break;
            
            case Sides.Right:
                
                if (currentBookState != BookState.End)
                {
                    if (currentPage == 0)
                    {
                        cover.GetComponent<PagePhysics>().TurnPage(turnSide, turnSpeed);
                    }

                    if (currentPage > 0)
                    {
                        pages[currentPage - 1].GetComponent<PagePhysics>().TurnPage(turnSide, turnSpeed);
                    }
                    
                    currentPage++;
                }
                else
                {
                    if(!pages[pages.Length - 1].GetComponent<PagePhysics>().isArrived) return;
                    // faire en sorte que toutes les pages tournent quand on est au debut/fin

                    
                    cover.GetComponent<PagePhysics>().TurnPage(Sides.Left, turnSpeed * 1.3f);
                    foreach (GameObject page in pages)
                    {
                        page.GetComponent<PagePhysics>().TurnPage(Sides.Left, turnSpeed * 1.3f);
                    }

                    currentPage = 0;
                }
                break;
        }
        
        
        if (currentPage == 0)
        {
            SetBookState(BookState.Start);
        }
        else if(currentPage == pages.Length + 1)
        {
            SetBookState(BookState.End);
        }
        else
        {
            SetBookState(BookState.Middle);
        }

        // DebugPage();
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


    public void HoldingBook()
    {
        isHeld = true;
    }
    
    public void DropingBook()
    {
        isHeld = false;
    }

    public void UseBook()
    {
        if (isHeld)
        {
            UpdatePage(Sides.Right);
        }
    }
    
}