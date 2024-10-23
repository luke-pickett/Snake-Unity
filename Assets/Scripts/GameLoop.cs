using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    [SerializeField] private GameObject _snakeHeadPrefab;
    
    void Start()
    {
        GridHandler.instance.PlaceObject( _snakeHeadPrefab, 0,0);
    }


    void Update()
    {
        
    }
}
