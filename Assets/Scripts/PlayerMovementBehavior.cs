using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementBehavior : MonoBehaviour
{
    private GameObject _snakeHead;
    private String _direction;
    public GameObject currentTile;

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
}
