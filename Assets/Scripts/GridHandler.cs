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
                _grid[i, j] = tile;
                pos += displacementY;
            }
            pos += displacementX;
        }
    }

    private void Start()
    {
        
    }

    public GameObject GrabTile(int xValue, int yValue)
    {
        if (xValue > gridSizeX || yValue > gridSizeY)
        {
            return null;
        }
        return _grid[xValue, yValue];
    }
}
