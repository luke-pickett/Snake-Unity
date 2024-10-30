using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    private int _score = 20;
    private float _turnTimer = 0f;

    private bool _timerEnabled = true;
    private bool _gameStarted = false; 
    private Coroutine _scoreDecrementCoroutine; 

    private float _destructionTimer = 0f;
    private bool _runDestruction = false;

    [SerializeField] private TextMeshProUGUI ScoreUI; 
    [SerializeField] private GameObject startUI; 
    private FruitSpawnBehaviour fruitSpawnBehaviour; 

    private void OnEnable()
    {
        PlayerMovementBehavior.PlayerHit += Gameover;
        FruitBehavior.FruitCollected += UpdateScore; 
        FruitBehavior.FruitCollected += SpeedUp;
    }

    void Start()
    {
        instance = this;

        // Set the game state inactive initially
        Time.timeScale = 0; 
        startUI.SetActive(true); 

        UpdateScoreUI(); 
    }

    void Update()
    {
        if (Input.anyKeyDown && !_gameStarted) 
        {
            _gameStarted = true; 
            startUI.SetActive(false); 
            Time.timeScale = 1;

            _snakeHead = GridHandler.instance.PlaceObject(snakeHeadPrefab, 0, 0);
            snake.Add(_snakeHead);
            _snakeHead.GetComponent<PlayerMovementBehavior>().coordinates = new[] { 0, 0 };
            _snakeHead.GetComponent<PlayerMovementBehavior>().snakeTailPrefab = snakeTailPrefab;

            // Start the score decrement coroutine
            _scoreDecrementCoroutine = StartCoroutine(DecreaseScoreOverTime());
        }

        if (_timerEnabled)
        {
            ProgressTurnTimer();
        }
    }

    void ProgressTurnTimer()
    {
        _turnTimer += Time.deltaTime;
        if (_turnTimer >= timeBetweenTurns)
        {
            ChangeTurn?.Invoke();
            _turnTimer = 0;
        }
    }

    void SpeedUp()
    {
        if (timeBetweenTurns >= 0.1f)
        {
            timeBetweenTurns -= 0.05f;
        }
    }

    void Gameover()
    {
        FruitSpawnBehaviour.instance.enabled = false;
        foreach (GameObject segment in snake)
        {
            segment.GetComponent<MovementBehavior>().enabled = false;
        }

        _timerEnabled = false;

        // Stop score decrement coroutine if it's running
        if (_scoreDecrementCoroutine != null)
        {
            StopCoroutine(_scoreDecrementCoroutine);
            _scoreDecrementCoroutine = null;
        }

        // Disable fruit spawning
        if (fruitSpawnBehaviour != null)
        {
            fruitSpawnBehaviour.enabled = false; 
        }

        gameObject.GetComponent<SnakeDestruction>().enabled = true;
    }

    private void UpdateScore()
    {
        _score += 5; 
        UpdateScoreUI();
    }

    private IEnumerator DecreaseScoreOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1); 
            _score = Mathf.Max(0, _score - 1); 
            UpdateScoreUI();
        }
    }

    private void UpdateScoreUI()
    {
        ScoreUI.text = "Score: " + _score; 
    }
}
