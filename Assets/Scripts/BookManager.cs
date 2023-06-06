using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class BookManager : MonoBehaviour
{
    public GameObject cover;
    public GameObject[] pages;
    [Range(100, 600)] public float turnSpeed = 100;
    [Range(0.1f, 3f)]public float closeSpeedMultiplier = 1.3f;
    private float pocketSpeedMultiplier = 6f;
    private int startingPage;
    
    private int currentPage;
    private BookState currentBookState = BookState.Start;

    private bool isHeld = false;
    
    [Range(0, 4)] public int availablePages = 3; 

    
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
        
        for (int i = 0; i < pages.Length; i++)
        {
            if (i > availablePages - 1)
            {
                pages[i].SetActive(false);
            }
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
                 
                    
                    CloseBook(Sides.Left);
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
                    CloseBook(Sides.Right);
                }
                break;
        }
        
        
        if (currentPage == 0)
        {
            SetBookState(BookState.Start);
        }
        else if(currentPage == availablePages + 1)
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

        if (currentPage != 0)
        {
            GoToPage(currentPage);
        }
    }
    
    public void DropingBook()
    {
        isHeld = false;
        CloseBook(Sides.Right, true);
    }

    public void UseBook()
    {
        if (isHeld)
        {
            UpdatePage(Sides.Right);
        }
    }

    private void CloseBook(Sides closeSide, bool registerCurrentPage = false)
    {
        Sides inverseSide = closeSide == Sides.Right? Sides.Left : Sides.Right;
        if (closeSide == Sides.Right)
        {
            if(!pages[availablePages - 1].GetComponent<PagePhysics>().isArrived) return;
            
            if(!registerCurrentPage) currentPage = 0;
        }
        else
        {
            if(!cover.GetComponent<PagePhysics>().isArrived) return;
            
            if(!registerCurrentPage) currentPage = availablePages + 1;
        }
        
        float turnSpeedMultiplier = registerCurrentPage ? pocketSpeedMultiplier : closeSpeedMultiplier;
        cover.GetComponent<PagePhysics>().TurnPage(inverseSide, turnSpeed * turnSpeedMultiplier);
        
        for (int i = 0; i < availablePages; i++)
        {
            pages[i].GetComponent<PagePhysics>().TurnPage(inverseSide, turnSpeed * turnSpeedMultiplier);
        }
    }
    private void GoToPage(int targetPage)
    {
        cover.GetComponent<PagePhysics>().TurnPage(Sides.Right, turnSpeed *pocketSpeedMultiplier);

        for (int i = 0; i < targetPage - 1; i++)
        {
            pages[i].GetComponent<PagePhysics>().TurnPage(Sides.Right, turnSpeed * pocketSpeedMultiplier);
        }
    }
    
    public void RevealPage()
    {
        pages[availablePages].SetActive(true);
        availablePages++;
    }

    public void RevealPageVariant(int targetPage)
    {
        // have a variant here and activate it 
        // targetPage - 1
    }
    
}