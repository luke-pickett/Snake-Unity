using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _fruit;
    [SerializeField] private float _spawnTimer;
    private float _timer;

    void Start()
    {
        
    }


    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _spawnTimer)
        {
            _timer = 0;
            GridHandler.instance.PlaceObject(_fruit, 0, 0);
        }
    }
}
