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
        
        switch (_direction)
        {
            case "north":
                if (surroundingTiles[0] != null)
                {
                    GameObject newCurrentTile = surroundingTiles[0];
                    TileProperties newCurrentTileProperties = newCurrentTile.GetComponent<TileProperties>();
                    _currentTileProperties.contains.Remove(_snakeHead);
                    newCurrentTileProperties.contains.Add(_snakeHead);
                    _snakeHead.transform.position = newCurrentTile.transform.position + new Vector3(0, 1, 0);
                    _currentTile = newCurrentTile;
                    _currentTileProperties = newCurrentTileProperties;
                    coordinates = new[] { _currentTileProperties.xValue, _currentTileProperties.yValue };

                }
                else
                {
                    PlayerHit?.Invoke();
                }
                return;
            case "east":
                if (surroundingTiles[1] != null)
                {
                    GameObject newCurrentTile = surroundingTiles[1];
                    TileProperties newCurrentTileProperties = newCurrentTile.GetComponent<TileProperties>();
                    _currentTileProperties.contains.Remove(_snakeHead);
                    newCurrentTileProperties.contains.Add(_snakeHead);
                    _snakeHead.transform.position = newCurrentTile.transform.position + new Vector3(0, 1, 0);
                    _currentTile = newCurrentTile;
                    _currentTileProperties = newCurrentTileProperties;
                    coordinates = new[] { _currentTileProperties.xValue, _currentTileProperties.yValue };
                }
                else
                {
                    PlayerHit?.Invoke();
                }
                return;
            case "south":
                if (surroundingTiles[2] != null)
                {
                    GameObject newCurrentTile = surroundingTiles[2];
                    TileProperties newCurrentTileProperties = newCurrentTile.GetComponent<TileProperties>();
                    _currentTileProperties.contains.Remove(_snakeHead);
                    newCurrentTileProperties.contains.Add(_snakeHead);
                    _snakeHead.transform.position = newCurrentTile.transform.position + new Vector3(0, 1, 0);
                    _currentTile = newCurrentTile;
                    _currentTileProperties = newCurrentTileProperties;
                    coordinates = new[] { _currentTileProperties.xValue, _currentTileProperties.yValue };
                }
                else
                {
                    PlayerHit?.Invoke();
                }
                return;
            case "west":
                if (surroundingTiles[3] != null)
                {
                    GameObject newCurrentTile = surroundingTiles[3];
                    TileProperties newCurrentTileProperties = newCurrentTile.GetComponent<TileProperties>();
                    _currentTileProperties.contains.Remove(_snakeHead);
                    newCurrentTileProperties.contains.Add(_snakeHead);
                    _snakeHead.transform.position = newCurrentTile.transform.position + new Vector3(0, 1, 0);
                    _currentTile = newCurrentTile;
                    _currentTileProperties = newCurrentTileProperties;
                    coordinates = new[] { _currentTileProperties.xValue, _currentTileProperties.yValue };
                }
                else
                {
                    PlayerHit?.Invoke();
                }
                return;
        }
    }
}
