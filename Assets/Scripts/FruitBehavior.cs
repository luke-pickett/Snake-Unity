using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FruitBehavior : MonoBehaviour
{
    private float _rotationSpeed = 50f; 
    public GameObject occupyingTile;
    private List<GameObject> _tileList;

    private Vector3 _targetScale;
    private float _growSpeed = 1f; // Speed at which the fruit grows
    private bool _isGrowing = true; // To control whether the fruit is growing or shrinking
    private bool _isActive = true; // To check if the fruit is active (can be eaten)

    public delegate void PlayerDetected();
    public static event PlayerDetected FruitCollected;

    private void OnEnable()
    {
        GameLoop.ChangeTurn += CheckIfEaten;
    }

    private void Update()
    {
        // Rotate the fruit
        transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);

        // Update the scale
        if (_isGrowing)
        {
            // Grow the fruit towards the target scale
            transform.localScale = Vector3.Lerp(transform.localScale, _targetScale, _growSpeed * Time.deltaTime);
        }
    }

    public void SetTargetScale(Vector3 targetScale)
    {
        _targetScale = targetScale;
    }

    private void CheckIfEaten()
    {
        if (!_isActive) return; // Prevent eating if the fruit is not active

        _tileList = occupyingTile.GetComponent<TileProperties>().contains;
        bool hasPlayer = _tileList.Cast<GameObject>().Any(obj => obj.CompareTag("Player"));
        if (hasPlayer)
        {
            _isActive = false; // Mark the fruit as inactive before starting to shrink
            _isGrowing = false; // Stop growing when eaten
            StartCoroutine(ShrinkAndDestroy());
            FruitCollected?.Invoke();
        }
    }

    private IEnumerator ShrinkAndDestroy()
    {
        // Shrink the fruit to invisible and then destroy it
        while (transform.localScale.x > 0.01f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, _growSpeed * Time.deltaTime);
            yield return null; // Wait until the next frame
        }
        Destroy(gameObject); // Destroy the fruit after shrinking
    }

    private void OnDisable()
    {
        GameLoop.ChangeTurn -= CheckIfEaten;
    }
}
