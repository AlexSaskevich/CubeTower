using System.Collections;
using UnityEngine;

public class SellZoneIndicator : Indicator
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _hideDistance;
    [SerializeField] private Stack _stack;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _stack.Fulled += OnFulled;
    }

    private void OnDisable()
    {
        _stack.Fulled -= OnFulled;
    }

    private void OnFulled()
    {
        StartShowIndicatorCoroutine();
    }

    private void StartShowIndicatorCoroutine()
    {
        if (_coroutine != null)
            StopCoroutine(ShowIndicatorCoroutine());

        _coroutine = StartCoroutine(ShowIndicatorCoroutine());
    }

    private IEnumerator ShowIndicatorCoroutine()
    {
        while (_stack.CurrentCapacity <= 1)
        {
            ShowIndicator(_target.position, _hideDistance);
            yield return null;
        }
    }
}