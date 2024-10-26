using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{

    public static GameLoop instance;
    [SerializeField] private GameObject snakeHeadPrefab;
    [SerializeField] private float timeBetweenTurns;

    public delegate void TurnTimeOver();
    public static event TurnTimeOver ChangeTurn;
    
    private GameObject _snakeHead;
    private List<GameObject> _snake = new List<GameObject>();

    private int _score = 0;
    private float timer = 0f;

    private void OnEnable()
    {
        PlayerMovementBehavior.PlayerHit += Gameover;
    }
    void Start()
    {
        instance = this;

        _snakeHead = GridHandler.instance.PlaceObject( snakeHeadPrefab, 0,0);
        _snake.Add(_snakeHead);
        _snakeHead.GetComponent<PlayerMovementBehavior>().coordinates = new[] { 0, 0 };
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenTurns)
        {
            ChangeTurn?.Invoke();
            timer = 0;
        }
    }

    void Gameover()
    {
        _snakeHead.GetComponent<PlayerMovementBehavior>().enabled = false;
    }
}
