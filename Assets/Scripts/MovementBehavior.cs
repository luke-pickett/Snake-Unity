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
    
    protected void ChangeTile(GameObject aheadTile, GameObject movingObject)
    {
        TileProperties aheadTileProperties = aheadTile.GetComponent<TileProperties>();
        currentTileProperties.contains.Remove(movingObject);
        aheadTileProperties.contains.Add(movingObject);
        movingObject.transform.position = aheadTile.transform.position + new Vector3(0, 1, 0);
        currentTile = aheadTile;
        currentTileProperties = aheadTileProperties;
        coordinates = new[] { currentTileProperties.xValue, currentTileProperties.yValue };
        secondPreviousDirection = firstPreviousDirection;
        firstPreviousDirection = direction;
    }
}
