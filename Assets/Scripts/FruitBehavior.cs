using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FruitBehavior : MonoBehaviour
{
    private float _rotationSpeed = 50f; 
    public GameObject occupyingTile;
    private List<GameObject> _tileList;

    public delegate void PlayerDetected();
    public static event PlayerDetected FruitCollected;

    private void OnEnable()
    {
        GameLoop.ChangeTurn += CheckIfEaten;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
    }

    private void CheckIfEaten()
    {
        _tileList = occupyingTile.GetComponent<TileProperties>().contains;
        // Checks if any object in the list contains a Gameobject with the tag "Player"
        bool hasPlayer = _tileList.Cast<GameObject>().Any(obj => obj.CompareTag("Player"));
        if (hasPlayer)
        {
            FruitCollected?.Invoke();
            Destroy(gameObject); 
        }
    }

    private void OnDisable()
    {
        GameLoop.ChangeTurn -= CheckIfEaten;
    }
}

