using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _padding = 1f; // Additional padding around the grid

    void Start()
    {
        SetCameraPosition();
    }

    void SetCameraPosition()
    {
        // Get grid dimensions from GridHandler
        int gridSizeX = GridHandler.instance.GetGridSizeX();
        int gridSizeY = GridHandler.instance.GetGridSizeY();

        // Calculate the center tile coordinates
        float centerX = (gridSizeX - 1) / 2f; 
        float centerY = (gridSizeY - 1) / 2f;

        // Calculate the world position of the middle tile
        Vector3 middleTilePosition = new Vector3(centerX, 0, centerY);

        // Calculate the camera height needed to fit the entire grid, adjusting for padding
        float height = gridSizeY + _padding; 
        float width = gridSizeX + _padding;  

        // Position the camera above the middle tile
        transform.position = new Vector3(middleTilePosition.x, height, middleTilePosition.z);
        transform.rotation = Quaternion.Euler(90f, 0f, 0f); 

    }
}
