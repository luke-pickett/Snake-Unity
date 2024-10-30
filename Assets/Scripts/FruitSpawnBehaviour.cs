using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FruitSpawnBehaviour : MonoBehaviour
{
    public static FruitSpawnBehaviour instance;
    
    [SerializeField] private GameObject[] _fruitGallery; 
    [SerializeField] private float _spawnTimer = 5f; // Initial spawn timer
    [SerializeField] private float _spawnReductionFactor = 0.95f; // Reduce timer by 5% every spawn
    [SerializeField] private float _minSpawnTime = 0.5f; // Minimum limit for spawn time
    [SerializeField] private float _scale = 20f; // Fruit scale multiplier

    private float _currentSpawnTime;
    private float _timer;

    void Start()
    {
        instance = this;
        _currentSpawnTime = _spawnTimer;
        _timer = 0;
    }

    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _currentSpawnTime)
        {
            _timer = 0;
            SpawnFruit();
            // Reduce the spawn timer, but don't go below the minimum spawn time
            _currentSpawnTime = Mathf.Max(_currentSpawnTime * _spawnReductionFactor, _minSpawnTime);
        }
    }

    void SpawnFruit()
    {
        // Get random position in grid
        int randomX = Random.Range(0, GridHandler.instance.GetGridSizeX());
        int randomY = Random.Range(0, GridHandler.instance.GetGridSizeY());

        // Calculate the position of the tile based on its grid coordinates
        GameObject targetTile = GridHandler.instance.GrabTile(randomX, randomY);
        if (targetTile == null)
        {
            Debug.LogWarning($"Target tile ({randomX}, {randomY}) does not exist.");
            return; // Exit if the tile does not exist
        }

        Vector3 targetTilePosition = targetTile.transform.position + new Vector3(0, 1, 0); // Adjust position

        // Select a random fruit prefab from the gallery
        GameObject fruitPrefab = _fruitGallery[Random.Range(0, _fruitGallery.Length)];

        // Instantiate the prefab and set its scale
        GameObject fruitObject = Instantiate(fruitPrefab, targetTilePosition, Quaternion.identity);
    
        // Get the FruitBehavior component
        FruitBehavior fruitBehavior = fruitObject.GetComponent<FruitBehavior>();
        if (fruitBehavior != null)
        {
            // Set the target scale in FruitBehavior
            fruitBehavior.SetTargetScale(Vector3.one * _scale); // Use the scale value
            fruitBehavior.occupyingTile = targetTile; // Assign the tile it occupies
        }
        else
        {
            Debug.LogWarning("FruitBehaviour component missing on fruit prefab.");
        }
    }
}
