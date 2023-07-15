using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;
using System.Linq;
using Unity.VisualScripting;


public class TaquinEnigmaManager : Enigma
{
    private GameObject[] allTaquins;
    
    
    private Dictionary<Vector2, TaquinTile> tiles = new Dictionary<Vector2, TaquinTile>();
    private bool heldTaquin;
    [SerializeField]
    private int maxMoves;
    private int movesLeft;
    
    
    private void Start()
    {
        //FAIRE UN CHECK DES COORDONNES DES TAQUINS POUR LA FIN
        //GARDER EN MEMOIRE LE POSITIONNEMENT DE BASE
        
        
        movesLeft = maxMoves;

        TaquinTile[] salut = FindObjectsOfType<TaquinTile>();
        //oblige de par ca sinon il accepte pas 
        allTaquins = new GameObject[salut.Length];
        for (int i = 0; i < salut.Length; i++)
        {
            allTaquins[i] = salut[i].gameObject;
        }

        //Plus forcement necessaire mais c'est propre + tjrs le meme output
        Array.Sort(allTaquins, (a,b) => a.name.CompareTo(b.name));

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
        return false;
    }

    public override void ValidEnigma()
    {
        
    }


    public void ValidTaquinPlaced(Vector2 coords)
    {
        movesLeft--;
        Debug.Log(movesLeft);

        //CHECK SI LES COORDS SONT BONNES + RETURN
        if (movesLeft > 0)
        { 
            AssignCoordinates(coords);
        }
        else
        {
            ResetTaquinPostions();
        }
        
    }

    void AssignCoordinates(Vector2 coords)
    {
        TaquinTile targetTaquinTile;
        tiles.TryGetValue(coords, out targetTaquinTile);
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