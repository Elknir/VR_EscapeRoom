using UnityEngine;

public class BookManager : MonoBehaviour
{
    public GameObject cover;
    public GameObject[] pages;
    public float turnSpeed = 100;
    private int startingPage;
    public float offsetBetweenPages = 0.1f;
    
    private int currentPage;
    private BookState currentBookState = BookState.Start;



    

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
        switch (turnSide)
        {
            case Sides.Left:
                
                
                
                if (currentBookState != BookState.Start)
                {
                    cover.GetComponent<PagePhysics>().TurnPage(turnSide, turnSpeed);


                    if (currentPage > 1)
                    {
                        pages[currentPage - 2].GetComponent<PagePhysics>().TurnPage(turnSide, turnSpeed);
                    }
                    
                    currentPage--;
                }
                else
                {
                    // C est pas bon avec la cover a check apres
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
        
        


        

        
        // TODO : search for switch alternative 
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


        DebugPage();

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