using UnityEngine;


public class BookManager : MonoBehaviour
{
    [Header("Pages Setup")]
    public GameObject cover, reliure;
    
    
    private PagePhysics coverPhysics, reliurePhysics;
    private PagePhysics[] targetPagePhysics;

    public GameObject[] pages;
    [Range(0, 4)] public int availablePages = 3; 

    [Header("Pages Speed")]
    [Range(100, 600)] public float turnSpeed = 100;
    [Range(50, 200)] public float reliureTurnSpeed = 100;
    [Range(0.1f, 3f)]public float closeSpeedMultiplier = 1.3f;
    readonly float pocketSpeedMultiplier = 10f;
    
    private int currentPage = 0;
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
        targetPagePhysics = new PagePhysics[pages.Length];
        for (int i = 0; i < pages.Length; i++)
        {
            targetPagePhysics[i] = pages[i].GetComponent<PagePhysics>();
            
            if (i > availablePages - 1)
            {
                pages[i].SetActive(false);
            }
        } 
        
        
        coverPhysics = cover.GetComponent<PagePhysics>();
        reliurePhysics = reliure.GetComponent<PagePhysics>();
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
                        coverPhysics.TurnPage(turnSide, turnSpeed);
                        reliurePhysics.TurnPage(turnSide, reliureTurnSpeed);
                    }
                    
                    if (currentPage > 1)
                    {
                        targetPagePhysics[currentPage - 2].TurnPage(turnSide, turnSpeed);
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
                        coverPhysics.TurnPage(turnSide, turnSpeed);
                        reliurePhysics.TurnPage(turnSide, reliureTurnSpeed);
                    }

                    if (currentPage > 0)
                    {
                        targetPagePhysics[currentPage - 1].TurnPage(turnSide, turnSpeed);
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

    public void HoldItem()
    {
        isHeld = true;
        if (currentPage != 0)
        {
            GoToPage(currentPage);
        }
    }
    
    public void DropItem()
    {
        isHeld = false;
        CloseBook(Sides.Right, true);
    }
    
    public void UseItem()
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
            if(!targetPagePhysics[availablePages - 1].isArrived) return;
            
            if(!registerCurrentPage) currentPage = 0;
        }
        else
        {
            if(!coverPhysics.isArrived) return;
            
            if(!registerCurrentPage) currentPage = availablePages + 1;
        }
        
        float turnSpeedMultiplier = registerCurrentPage ? pocketSpeedMultiplier : closeSpeedMultiplier;
        coverPhysics.TurnPage(inverseSide, turnSpeed * turnSpeedMultiplier);
        reliurePhysics.TurnPage(inverseSide, reliureTurnSpeed * turnSpeedMultiplier);
        
        for (int i = 0; i < availablePages; i++)
        {
            targetPagePhysics[i].TurnPage(inverseSide, turnSpeed * turnSpeedMultiplier);
        }
    }
    private void GoToPage(int targetPage)
    {
        coverPhysics.TurnPage(Sides.Right, turnSpeed *pocketSpeedMultiplier);
        reliurePhysics.TurnPage(Sides.Right, reliureTurnSpeed *pocketSpeedMultiplier);
        
        for (int i = 0; i < targetPage - 1; i++)
        {
            targetPagePhysics[i].TurnPage(Sides.Right, turnSpeed * pocketSpeedMultiplier);
        }
    }
    
    
    // TODO : Reveal book pages 
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