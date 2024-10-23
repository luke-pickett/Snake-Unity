using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementBehavior : MonoBehaviour
{
    private GameObject _snakeHead;
    private String _direction;
    public int[] coordinates;

    private void OnEnable()
    {
        
    }

    private void Start()
    {
        _snakeHead = gameObject;
    }

    private void Update()
    {
        // Controls
        if (Input.GetKeyDown(KeyCode.W))
        {
            _direction = "north";
        }
        else if ((Input.GetKeyDown(KeyCode.D)))
        {
            _direction = "east";
        }
        else if ((Input.GetKeyDown(KeyCode.S)))
        {
            _direction = "south";
        }
        else if ((Input.GetKeyDown(KeyCode.A)))
        {
            _direction = "west";
        }
    }

    private void Move()
    {
        GameObject[] surroundingTiles = GridHandler.instance.GrabAdjacentTiles(
            coordinates[0], coordinates[1]
            );
        
        switch (_direction)
        {
            case "north":
                if (surroundingTiles[0] != null)
                {
                    
                }

                return;
        }
    }
}
