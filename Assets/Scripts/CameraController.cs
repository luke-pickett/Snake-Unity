using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _cameraHeight = 10f; // Adjustable camera height

    void Start()
    {
        if (GridHandler.instance != null)
        {
            SetCameraPosition(); 
        }
    }

    void SetCameraPosition()
    {
        // Get grid dimensions from GridHandler
        int gridSizeX = GridHandler.instance.GetGridSizeX();
        int gridSizeY = GridHandler.instance.GetGridSizeY();

        // Calculate the center tile coordinates
        float centerX = (gridSizeX - 1) / 2f; // Middle index for zero-based grid
        float centerY = (gridSizeY - 1) / 2f;

        // Calculate the world position of the middle tile
        Vector3 middleTilePosition = new Vector3(centerX, 0, centerY);

        // Set the camera position above the middle tile, looking straight down
        transform.position = new Vector3(middleTilePosition.x, _cameraHeight, middleTilePosition.z);
        transform.rotation = Quaternion.Euler(90f, 0f, 0f); // Look straight down
    }
}
