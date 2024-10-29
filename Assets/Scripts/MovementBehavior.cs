using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MovementBehavior : MonoBehaviour
{
    public int[] coordinates;
    public GameLoop.Direction direction = GameLoop.Direction.None;
    public GameLoop.Direction firstPreviousDirection = GameLoop.Direction.None;
    public GameLoop.Direction secondPreviousDirection = GameLoop.Direction.None;
    
    public GameObject currentTile;
    public TileProperties currentTileProperties;
}
