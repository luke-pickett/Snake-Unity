using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TailBehavior : MovementBehavior
{
    private PlayerMovementBehavior _snakeHeadBehavior;

    private GameObject _tailSegment;

    private void OnEnable()
    {
        GameLoop.ChangeTurn += Move;
    }

    private void Start()
    {
        _snakeHeadBehavior = GameLoop.instance.snake[0].GetComponent<PlayerMovementBehavior>();
        
        _tailSegment = gameObject;
        
        currentTile = GridHandler.instance.GrabTile(coordinates[0], coordinates[1]);
        currentTileProperties = currentTile.GetComponent<TileProperties>();
    }

    private GameLoop.Direction FindNewDirection()
    {
        List<GameObject> snakeSegments = GameLoop.instance.snake;
        int currentSegmentIndex = snakeSegments.IndexOf(this.gameObject);
        GameObject nextLeadingSegment = snakeSegments[currentSegmentIndex - 1];
        MovementBehavior nextLeadingSegmentBehavior = nextLeadingSegment.GetComponent<MovementBehavior>();
        return nextLeadingSegmentBehavior.secondPreviousDirection;
    }

    private void Move()
    {
        direction = FindNewDirection();
        GameObject[] surroundingTiles = GridHandler.instance.GrabAdjacentTiles(
            coordinates[0], coordinates[1]
        );
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

        bool validDirection = (direction != GameLoop.Direction.None);
        bool tileAheadExists = (aheadTile != null);
        if (validDirection && tileAheadExists)
        {
            aheadTileProperties = aheadTile.GetComponent<TileProperties>();
            currentTileProperties.contains.Remove(_tailSegment);
            aheadTileProperties.contains.Add(_tailSegment);
            _tailSegment.transform.position = aheadTile.transform.position + new Vector3(0, 1, 0);
            currentTile = aheadTile;
            currentTileProperties = aheadTileProperties;
            coordinates = new[] { currentTileProperties.xValue, currentTileProperties.yValue };
            secondPreviousDirection = firstPreviousDirection;
            firstPreviousDirection = direction;
        }
    }
}
