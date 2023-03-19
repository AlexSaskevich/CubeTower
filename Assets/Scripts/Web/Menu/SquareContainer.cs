using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SquareContainer : MonoBehaviour
{
    [SerializeField] private SquareSpawner _spawner;

    private readonly List<GameObject> _squares = new();

    public event UnityAction ContainerCleared;

    public static SquareContainer Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void TryAdd(GameObject square)
    {
        if (_squares.Contains(square))
            return;

        if (_squares.Count >= _spawner.SquareCount)
            return;

        _squares.Add(square);

        if (_squares.Count == _spawner.SquareCount)
            Clear();
    }

    private void Clear()
    {
        foreach (var square in _squares)
            square.GetComponent<Square>().StopMove();

        _squares.Clear();

        ContainerCleared?.Invoke();
    }
}