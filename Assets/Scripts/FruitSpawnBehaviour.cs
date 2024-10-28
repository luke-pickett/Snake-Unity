using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FruitSpawnBehaviour : MonoBehaviour
{
    [SerializeField] private Mesh[] _fruitGallery; // Array of fruit meshes
    [SerializeField] private float _spawnTimer = 5f; // Initial spawn timer
    [SerializeField] private float _spawnReductionFactor = 0.95f; // Reduce timer by 5% every spawn
    [SerializeField] private float _minSpawnTime = 0.5f; // Minimum limit for spawn time

    [SerializeField] private IDictionary<Mesh, Material> _meshMaterials = new Dictionary<Mesh, Material>();
    
    private float _currentSpawnTime;
    private float _timer;

    void Start()
    {
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

        // Select random fruit from the gallery
        //GameObject randomFruit = _fruitGallery[Random.Range(0, _fruitGallery.Length)];

        // Place the fruit at the random grid position
        //GridHandler.instance.PlaceObject(randomFruit, randomX, randomY);
    }
}