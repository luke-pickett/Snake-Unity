using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementBehavior : MonoBehaviour
{
    private GameObject _snakeHead;

    enum Direction
    {
        North,
        East,
        South,
        West
    }
    private Direction _direction = Direction.North;
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
            _direction = Direction.North;
        }
        else if ((Input.GetKeyDown(KeyCode.D)))
        {
            _direction = Direction.East;
        }
        else if ((Input.GetKeyDown(KeyCode.S)))
        {
            _direction = Direction.South;
        }
        else if ((Input.GetKeyDown(KeyCode.A)))
        {
            _direction = Direction.West;
        }
    }

    private void Move()
    {
        GameObject[] surroundingTiles = GridHandler.instance.GrabAdjacentTiles(
            coordinates[0], coordinates[1]
            );
        GameObject newCurrentTile = null;
        GameObject aheadTile = null;
        TileProperties aheadTileProperties = null;
        switch (_direction)
        {
            case Direction.North:
                aheadTile = surroundingTiles[0];
                break;
            case Direction.East:
                aheadTile = surroundingTiles[1];
                break;
            case Direction.South:
                aheadTile = surroundingTiles[2];
                break;
            case Direction.West:
                aheadTile = surroundingTiles[3];
                break;
        }
        if (aheadTile == null)
        {
            PlayerHit?.Invoke();
            return;
        }
        aheadTileProperties = aheadTile.GetComponent<TileProperties>();
        List<GameObject> aheadContainedList = aheadTileProperties.contains;
        // Sees if any Gameobject in the list has the tag "Player"
        bool hasPlayer = aheadContainedList.Cast<GameObject>().Any(obj => obj.CompareTag("Player"));
        if (hasPlayer)
        {
            PlayerHit?.Invoke();
            return;
        }

        newCurrentTile = aheadTile;
        
        TileProperties newCurrentTileProperties = newCurrentTile.GetComponent<TileProperties>();
        _currentTileProperties.contains.Remove(_snakeHead);
        newCurrentTileProperties.contains.Add(_snakeHead);
        _snakeHead.transform.position = newCurrentTile.transform.position + new Vector3(0, 1, 0);
        _currentTile = newCurrentTile;
        _currentTileProperties = newCurrentTileProperties;
        coordinates = new[] { _currentTileProperties.xValue, _currentTileProperties.yValue };
    }

    private void AddToSnake()
    {
        
    }
}
