using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.XR.Interaction.Toolkit;


public class TaquinEnigmaManager : Enigma
{
    private GameObject[] allTaquins;
    
    private Dictionary<Vector2, TaquinTile> tiles = new Dictionary<Vector2, TaquinTile>();
    private bool heldTaquin;
    [SerializeField]
    private int maxMoves;
    private int movesLeft;
    
    private Vector2[] coordsGoals = new Vector2[]
    {
        //1
        new Vector2(0,0), 
        //2
        new Vector2(2,1),
        //3
        new Vector2(1,0),
        //4
        new Vector2(0,1),
        //6
        new Vector2(1,2),
        //7
        new Vector2(0,2),
        //8
        new Vector2(2,2),
        //9
        new Vector2(2,0), 
    };
    
    private void Start()
    {
        //FAIRE UN CHECK DES COORDONNES DES TAQUINS POUR LA FIN
        //GARDER EN MEMOIRE LE POSITIONNEMENT DE BASE
        
        
        movesLeft = maxMoves;

        TaquinTile[] tempTiles = FindObjectsOfType<TaquinTile>();
        //oblige de par ca sinon il accepte pas 
        allTaquins = new GameObject[tempTiles.Length];
        for (int i = 0; i < tempTiles.Length; i++)
        {
            allTaquins[i] = tempTiles[i].gameObject;
        }

        //Plus forcement necessaire mais c'est propre + tjrs le meme output
        Array.Sort(allTaquins, (a,b) => a.name.CompareTo(b.name));

        
        for (int i = 0; i < allTaquins.Length; i++)
        {
            allTaquins[i].GetComponent<TaquinTile>().goalCoords = coordsGoals[i];
        }
        
        DictionnaryInit();
    }


    private void DictionnaryInit()
    {
        foreach (var element in allTaquins)
        {
            int myNumber;
            int.TryParse(element.name.Last().ToString(), out myNumber);

            Vector2 tileCoords = new Vector2((myNumber - 1) % 3 , (myNumber - 1) / 3);
            // Debug.Log(element.name + " : "+ tileCoords.x + "/" + tileCoords.y);
            TaquinTile scriptTarget = element.GetComponent<TaquinTile>();
            
            scriptTarget.coordinates = tileCoords;
            tiles.Add(tileCoords,scriptTarget);
        }
        
        AssignDirections(new Vector2(1,1));

    } 


    public void TileGrabbed(Action validateGrab)
    {
        if (!heldTaquin)
        {
            heldTaquin = true;
            validateGrab();
        }
    }
    public void TileDropped(Action validateGrab)
    {
        heldTaquin = false;
        validateGrab();
    }


    protected override bool EnigmaCondition()
    {
        foreach (var elements in tiles)
        {
            // Debug.Log($"{elements.Value.coordinates != elements.Value.goalCoords} : {elements.Value.coordinates.x},{elements.Value.coordinates.y}/{elements.Value.goalCoords.x},{elements.Value.goalCoords.y}" );
            if (elements.Value.coordinates != elements.Value.goalCoords)
            {
                return false;
            }
        }
        return true;
    }

    public override void ValidEnigma()
    {
        foreach (var VARIABLE in tiles)
        {
            Debug.Log("Taquin locked !");
            var element = VARIABLE.Value;
            element.movingDirection = DirectionEnum.None;
        }
        
        var centerTaquinHolder =FindObjectsOfType<CenterTaquinHolder>();;
        if (centerTaquinHolder.Length == 0) return;
        centerTaquinHolder[0].gameObject.GetComponent<XRSocketInteractor>().socketActive = true;
    }


    public void ValidTaquinPlaced(Vector2 coords)
    {
        movesLeft--;
        // Debug.Log(movesLeft);
        AssignCoordinates(coords);
        
        CheckEnigmaValidation(currentEnigma);
        if(EnigmaCondition()) return;

        if (movesLeft <= 0)
        { 
            ResetTaquinPostions();
        }
        
    }

    void AssignCoordinates(Vector2 coords)
    {
        TaquinTile targetTaquinTile;
        tiles.TryGetValue(coords, out targetTaquinTile);
        if(!targetTaquinTile) return;
        
        switch (targetTaquinTile.movingDirection)
        {
            case DirectionEnum.Down:
                targetTaquinTile.coordinates = coords + Vector2.up;
                break;
            case DirectionEnum.Up:
                targetTaquinTile.coordinates = coords + Vector2.down;
                break;
            case DirectionEnum.Left:
                targetTaquinTile.coordinates = coords + Vector2.left;
                break;
            case DirectionEnum.Right:
                targetTaquinTile.coordinates =  coords + Vector2.right;
                break;
            default:
                break;
        }
        tiles.Remove(coords);
        tiles.Add(targetTaquinTile.coordinates, targetTaquinTile);
        AssignDirections(coords);
    }


    void AssignDirections(Vector2 coords)
    {
        foreach (var elements in tiles)
        {
            elements.Value.movingDirection = DirectionEnum.None;
        }

        TaquinTile targetTaquinTile;
        
        //dunno why its switched in x direction = to check why
        tiles.TryGetValue(coords + Vector2.left, out targetTaquinTile);
        if (targetTaquinTile) targetTaquinTile.movingDirection = DirectionEnum.Right;
        
        tiles.TryGetValue(coords + Vector2.right, out targetTaquinTile);
        if (targetTaquinTile) targetTaquinTile.movingDirection = DirectionEnum.Left;
        
        tiles.TryGetValue(coords + Vector2.down, out targetTaquinTile);
        if (targetTaquinTile) targetTaquinTile.movingDirection = DirectionEnum.Down;

        tiles.TryGetValue(coords + Vector2.up, out targetTaquinTile);
        if (targetTaquinTile) targetTaquinTile.movingDirection = DirectionEnum.Up;
    }


    void ResetTaquinPostions()
    {
        //Reset :
        foreach (var element in tiles)
        {
            //positions
            element.Value.transform.localPosition = Vector3.zero;
            element.Value.lockedPosition = element.Value.transform.position;
        }
        
        //coords
        tiles.Clear();
        DictionnaryInit();
        //directions
        AssignDirections(new Vector2(1,1));

        //nombre de coups
        movesLeft = maxMoves;

        heldTaquin = false;
    }
}