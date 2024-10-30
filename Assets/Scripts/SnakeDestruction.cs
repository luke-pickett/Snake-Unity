using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeDestruction : MonoBehaviour
{
    private List<GameObject> _snake;
    private float _timer = 0f;

    private void Start()
    {
        _snake = GameLoop.instance.snake;
        _snake.Reverse();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= 0.5f)
        {
            _timer = 0f;
            Destroy(_snake[0]);
            _snake.RemoveAt(0);
        }

        if (_snake.Count == 0)
        {
            enabled = false;
        }
        
    }
}
