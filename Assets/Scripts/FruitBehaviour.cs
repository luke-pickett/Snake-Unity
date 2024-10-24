using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject[] _fruitGallery; // Array of fruit meshes
    [SerializeField] private float _spawnTimer;
    private float _timer = 0;

    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _spawnTimer)
        {
            _timer = 0;
            SpawnFruit();
        }
    }

    void SpawnFruit()
    {
        // Get random position in grid
        int randomX = Random.Range(0, GridHandler.instance.gridSizeX);
        int randomY = Random.Range(0, GridHandler.instance.gridSizeY);

        // Select random fruit from the gallery
        GameObject randomFruit = _fruitGallery[Random.Range(0, _fruitGallery.Length)];

        // Place the fruit at the random grid position
        GridHandler.instance.PlaceObject(randomFruit, randomX, randomY);
    }
}
