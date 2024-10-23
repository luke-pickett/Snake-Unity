using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    [SerializeField] private GameObject _snakeHeadPrefab;
    // Start is called before the first frame update
    void Start()
    {
        GridHandler.instance.PlaceObject( _snakeHeadPrefab, 0,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
