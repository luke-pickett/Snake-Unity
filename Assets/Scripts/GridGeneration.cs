using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GridGeneration : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject gridParent;
    public int gridSizeX;
    public int gridSizeY;
    private float _spaceBetweenTiles;

    private void Awake()
    {
        _spaceBetweenTiles = tilePrefab.transform.localScale.x;
    }

    private void Start()
    {
        Vector3 pos = new Vector3(0, 0, 0);
        Vector3 displacementX = new Vector3(_spaceBetweenTiles, 0, 0);
        Vector3 displacementY = new Vector3(0, 0, _spaceBetweenTiles);
        for (int i = 0; i < gridSizeX; i++)
        {
            pos = new Vector3(pos.x, pos.y, 0);
            for (int j = 0; j < gridSizeY; j++)
            {
                GameObject tile = Instantiate(tilePrefab, pos, new Quaternion(), gridParent.transform);
                pos += displacementY;
            }
            pos += displacementX;
        }
    }
}
