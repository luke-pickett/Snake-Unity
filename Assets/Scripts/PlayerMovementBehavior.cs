using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovementBehavior : MovementBehavior
{
    private GameObject _snakeHead;

    private AudioSource _audioSource;

    public GameObject snakeTailPrefab;

    public delegate void HeadCollided();

    public static event HeadCollided PlayerHit;

    private void OnEnable()
    {
        GameLoop.ChangeTurn += Move;
        FruitBehavior.FruitCollected += AddToSnake;
        FruitBehavior.FruitCollected += PlayCollectionSound;
    }

    private void Start()
    {
        coordinates = new[] { 0, 0 };
        direction = GameLoop.Direction.North;
        
        _snakeHead = gameObject;
        currentTile = GridHandler.instance.GrabTile(coordinates[0], coordinates[1]);
        currentTileProperties = currentTile.GetComponent<TileProperties>();

        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Controls
        if (Input.GetKeyDown(KeyCode.W) && firstPreviousDirection != GameLoop.Direction.South)
        {
            direction = GameLoop.Direction.North;
        }
        else if ((Input.GetKeyDown(KeyCode.D) && firstPreviousDirection != GameLoop.Direction.West))
        {
            direction = GameLoop.Direction.East;
        }
        else if ((Input.GetKeyDown(KeyCode.S) && firstPreviousDirection != GameLoop.Direction.North))
        {
            direction = GameLoop.Direction.South;
        }
        else if ((Input.GetKeyDown(KeyCode.A) && firstPreviousDirection != GameLoop.Direction.East))
        {
            direction = GameLoop.Direction.West;
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
        switch (direction)
        {
            case GameLoop.Direction.North:
                aheadTile = surroundingTiles[0];
                break;
            case GameLoop.Direction.East:
                aheadTile = surroundingTiles[1];
                break;
            case GameLoop.Direction.South:
                aheadTile = surroundingTiles[2];
                break;
            case GameLoop.Direction.West:
                aheadTile = surroundingTiles[3];
                break;
        }
        if (aheadTile == null)
        {
            PlayerHit?.Invoke();
            return;
        }
        if (GridHandler.instance.TileContainsPlayer(aheadTile))
        {
            PlayerHit?.Invoke();
            return;
        }

        newCurrentTile = aheadTile;
        
        TileProperties newCurrentTileProperties = newCurrentTile.GetComponent<TileProperties>();
        currentTileProperties.contains.Remove(_snakeHead);
        newCurrentTileProperties.contains.Add(_snakeHead);
        _snakeHead.transform.position = newCurrentTile.transform.position + new Vector3(0, 1, 0);
        currentTile = newCurrentTile;
        currentTileProperties = newCurrentTileProperties;
        coordinates = new[] { currentTileProperties.xValue, currentTileProperties.yValue };
        secondPreviousDirection = firstPreviousDirection;
        firstPreviousDirection = direction;
    }

    private void AddToSnake()
    {
        GameObject furthestBack = GameLoop.instance.snake.Last();
        GameLoop.Direction furthestBackDirection = furthestBack.GetComponent<MovementBehavior>().direction;
        GameObject[] surroundingTiles = GridHandler.instance.GrabAdjacentTiles(
            furthestBack.GetComponent<MovementBehavior>().currentTile
        );

        GameObject targetTile = null;
        switch (furthestBackDirection)
        {
            case GameLoop.Direction.North:
                targetTile = surroundingTiles[2];
                break;
            case GameLoop.Direction.East:
                targetTile = surroundingTiles[3];
                break;
            case GameLoop.Direction.South:
                targetTile = surroundingTiles[0];
                break;
            case GameLoop.Direction.West:
                targetTile = surroundingTiles[1];
                break;
        }

        bool tileExists = (targetTile != null);
        if (tileExists)
        {
            GameObject newSegment = GridHandler.instance.PlaceObject(snakeTailPrefab, targetTile);
            TailBehavior newSegmentBehavior = newSegment.GetComponent<TailBehavior>();
            newSegmentBehavior.coordinates = GridHandler.instance.GrabCoordinates(targetTile);
            GameLoop.instance.snake.Add(newSegment);
        }
    }

    private void PlayCollectionSound()
    {
        _audioSource.Play();
    }
}
