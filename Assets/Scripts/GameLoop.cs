using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameLoop : MonoBehaviour
{

    public static GameLoop instance;
    [SerializeField] private GameObject snakeHeadPrefab;
    [SerializeField] private GameObject snakeTailPrefab;
    [SerializeField] private float timeBetweenTurns;

    public delegate void TurnTimeOver();
    public static event TurnTimeOver ChangeTurn;
    
    private GameObject _snakeHead;
    public List<GameObject> snake = new List<GameObject>();
    
    public enum Direction { North, East, South, West, None }

    private int _score = 0;
    private float _timer = 0f;

    private bool _timerEnabled = true;

    private void OnEnable()
    {
        PlayerMovementBehavior.PlayerHit += Gameover;
    }
    void Start()
    {
        instance = this;

        _snakeHead = GridHandler.instance.PlaceObject( snakeHeadPrefab, 0,0);
        snake.Add(_snakeHead);
        _snakeHead.GetComponent<PlayerMovementBehavior>().coordinates = new[] { 0, 0 };
        _snakeHead.GetComponent<PlayerMovementBehavior>().snakeTailPrefab = snakeTailPrefab;
    }
    void Update()
    {
        if (_timerEnabled)
        {
            ProgressTimer();
        }
    }

    void ProgressTimer()
    {
        _timer += Time.deltaTime;
        if (_timer >= timeBetweenTurns)
        {
            ChangeTurn?.Invoke();
            _timer = 0;
        }
    }

    void Gameover()
    {
        foreach (GameObject segment in snake)
        {
            segment.GetComponent<MovementBehavior>().enabled = false;
        }
        
        _timerEnabled = false;
        foreach (GameObject segment in snake)
        {
            StartCoroutine(DestroySegment(segment));
        }
    }

    private IEnumerator DestroySegment(GameObject segment)
    {
        yield return new WaitForSeconds(1);
        Destroy(segment);
    }
}
