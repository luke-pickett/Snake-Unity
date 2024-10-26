using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.Serialization;

public class GridHandler : MonoBehaviour
{
    public static GridHandler instance;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private int gridSizeX;
    [SerializeField] private int gridSizeY;
    private float _spaceBetweenTiles;
    private GameObject[,] _grid;

    private void Awake()
    {
        instance = this;
        _spaceBetweenTiles = tilePrefab.transform.localScale.x;
        _grid = new GameObject[gridSizeX, gridSizeY];
        
        GameObject gridParent = new GameObject();
        gridParent.name = "Grid";
        
        Vector3 pos = new Vector3(0, 0, 0);
        Vector3 displacementX = new Vector3(_spaceBetweenTiles, 0, 0);
        Vector3 displacementY = new Vector3(0, 0, _spaceBetweenTiles);
        for (int i = 0; i < gridSizeX; i++)
        {
            pos = new Vector3(pos.x, pos.y, 0);
            for (int j = 0; j < gridSizeY; j++)
            {
                GameObject tile = Instantiate(tilePrefab, pos, new Quaternion(), gridParent.transform);
                tile.name = $"({i},{j})";
                TileProperties properties = tile.AddComponent<TileProperties>();
                properties.xValue = i;
                properties.yValue = j;
                _grid[i, j] = tile;
                pos += displacementY;
            }
            pos += displacementX;
        }
    }

    // Public getters for grid size
    public int GetGridSizeX()
    {
        return gridSizeX;
    }

    public int GetGridSizeY()
    {
        return gridSizeY;
    }

    public GameObject GrabTile(int xValue, int yValue)
    {
        if (xValue < 0 || xValue > gridSizeX || yValue < 0 || yValue > gridSizeY)
        {
            return null;
        }
        return _grid[xValue, yValue];
    }

    public bool IsEdge(int xValue, int yValue)
    {
        if (xValue <= 0 || xValue >= gridSizeX-1 || yValue <= 0 || yValue >= gridSizeY-1)
        {
            return true;
        }

        return false;
    }

    public GameObject GrabMiddleTile()
    {
        int rows = gridSizeX - 1;
        int col = gridSizeY - 1;
        
        return _grid[(int) rows / 2, (int) col / 2];
    }
    
    // Returns an array of tiles to north, east, south, and west, in that order
    // if the tile is on the edge it will fill the tile that does not exist with null
    public GameObject[] GrabAdjacentTiles(int xValue, int yValue)
    {
        GameObject[] returnArray = new GameObject[4];
        // Grabs North tile
        if (yValue < gridSizeY-1)
        {
            returnArray[0] = GrabTile(xValue, yValue + 1);
        }
        else
        {
            returnArray[0] = null;
        }
        
        // Grabs East tile
        if (xValue < gridSizeX - 1)
        {
            returnArray[1] = GrabTile(xValue + 1, yValue);
        }
        else
        {
            returnArray[1] = null;
        }
        
        // Grabs South tile
        if (yValue > 0)
        {
            returnArray[2] = GrabTile(xValue, yValue-1);
        }
        else
        {
            returnArray[2] = null;
        }
        
        // Grabs West tile
        if (xValue > 0)
        {
            returnArray[3] = GrabTile(xValue - 1, yValue);
        }
        else
        {
            returnArray[3] = null;
        }
        return returnArray;
    }
    public GameObject[] GrabAdjacentTiles(GameObject tile)
    {
        TileProperties tileProperties = tile.GetComponent<TileProperties>();
        int xValue = tileProperties.xValue;
        int yValue = tileProperties.yValue;
        GameObject[] returnArray = new GameObject[4];
        // Grabs North tile
        if (yValue < gridSizeY-1)
        {
            returnArray[0] = GrabTile(xValue, yValue + 1);
        }
        else
        {
            returnArray[0] = null;
        }
        
        // Grabs East tile
        if (xValue < gridSizeX - 1)
        {
            returnArray[1] = GrabTile(xValue + 1, yValue);
        }
        else
        {
            returnArray[1] = null;
        }
        
        // Grabs South tile
        if (yValue > 0)
        {
            returnArray[2] = GrabTile(xValue, yValue-1);
        }
        else
        {
            returnArray[2] = null;
        }
        
        // Grabs West tile
        if (xValue > 0)
        {
            returnArray[3] = GrabTile(xValue - 1, yValue);
        }
        else
        {
            returnArray[3] = null;
        }
        return returnArray;
    }

    public GameObject PlaceObject(GameObject obj, int xValue, int yValue)
    {
        GameObject targetTile = GrabTile(xValue, yValue);
        Vector3 targetTilePos = targetTile.transform.position;
        TileProperties targetTileProperties = targetTile.GetComponent<TileProperties>();
        GameObject createdObj = Instantiate(obj, targetTilePos += new Vector3(0, 1, 0), new Quaternion());
        targetTileProperties.contains.Add(createdObj);
        return createdObj;
    }
    public GameObject PlaceObject(GameObject obj, GameObject tile)
    {
        GameObject targetTile = tile;
        Vector3 targetTilePos = targetTile.transform.position;
        TileProperties targetTileProperties = targetTile.GetComponent<TileProperties>();
        GameObject createdObj = Instantiate(obj, targetTilePos += new Vector3(0, 1, 0), new Quaternion());
        targetTileProperties.contains.Add(createdObj);
        return createdObj;
    }

    public void RemoveObjects(int xValue, int yValue)
    {
        GameObject targetTile = GrabTile(xValue, yValue);
        foreach (GameObject obj in targetTile.GetComponent<TileProperties>().contains)
        {
            Destroy(obj);
        }
    }
    public void RemoveObjects(GameObject tile)
    {
        GameObject targetTile = tile;
        foreach (GameObject obj in targetTile.GetComponent<TileProperties>().contains)
        {
            Destroy(obj);
        }
    }
    public void RemoveObjects(int xValue, int yValue, GameObject exception)
    {
        GameObject targetTile = GrabTile(xValue, yValue);
        foreach (GameObject obj in targetTile.GetComponent<TileProperties>().contains)
        {
            if (obj != exception)
            {
                Destroy(obj);
            }
        }
    }
    public void RemoveObjects(GameObject tile, GameObject exception)
    {
        GameObject targetTile = tile;
        foreach (GameObject obj in targetTile.GetComponent<TileProperties>().contains)
        {
            if (obj != exception)
            {
                Destroy(obj);
            }
        }
    }
    

    private void Start()
    {
        
    }
}
