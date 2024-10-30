using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeDestruction : MonoBehaviour
{
    [SerializeField] private float timeBetweenDestruction;
    
    private List<GameObject> _snake;
    private float _timer = 0f;

    private AudioSource _audioSource;

    private void Start()
    {
        _snake = GameLoop.instance.snake;
        _snake.Reverse();

        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= timeBetweenDestruction)
        {
            _timer = 0f;
            Destroy(_snake[0]);
            _snake.RemoveAt(0);
            _audioSource.Play();
        }

        if (_snake.Count == 0)
        {
            enabled = false;
        }
        
    }
}
