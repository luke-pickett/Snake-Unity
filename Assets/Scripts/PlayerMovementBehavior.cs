using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementBehavior : MonoBehaviour
{
    private GameObject _snakeHead;
    private String _direction = "north";
    public int[] coordinates;
    private GameObject _currentTile;
    private TileProperties _currentTileProperties;

    public delegate void HeadCollided();

    public static event HeadCollided PlayerHit;

    private void OnEnable()
    {
        GameLoop.ChangeTurn += Move;
    }

    private void Start()
    {
        GameLoop gameLoop = GameLoop.instance;
        _snakeHead = gameObject;
        _currentTile = GridHandler.instance.GrabTile(coordinates[0], coordinates[1]);
        _currentTileProperties = _currentTile.GetComponent<TileProperties>();
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
        GameObject newCurrentTile = _currentTile;
        switch (_direction)
        {
            
            case "north":
                if (surroundingTiles[0] == null)
                {
                    PlayerHit?.Invoke();
                    return;
                }
                else
                {
                    newCurrentTile = surroundingTiles[0];
                    break;
                }
            case "east":
                if (surroundingTiles[1] == null)
                {
                    PlayerHit?.Invoke();
                    return;
                }
                else
                {
                    newCurrentTile = surroundingTiles[1];
                    break;
                }
            case "south":
                if (surroundingTiles[2] == null)
                {
                    PlayerHit?.Invoke();
                    return;
                }
                else
                {
                    newCurrentTile = surroundingTiles[2];
                    break;
                }
            case "west":
                if (surroundingTiles[3] == null)
                {
                    PlayerHit?.Invoke();
                    return;
                }
                else
                {
                    newCurrentTile = surroundingTiles[3];
                    break;
                }
        }
        TileProperties newCurrentTileProperties = newCurrentTile.GetComponent<TileProperties>();
        _currentTileProperties.contains.Remove(_snakeHead);
        newCurrentTileProperties.contains.Add(_snakeHead);
        _snakeHead.transform.position = newCurrentTile.transform.position + new Vector3(0, 1, 0);
        _currentTile = newCurrentTile;
        _currentTileProperties = newCurrentTileProperties;
        coordinates = new[] { _currentTileProperties.xValue, _currentTileProperties.yValue };
    }
}
