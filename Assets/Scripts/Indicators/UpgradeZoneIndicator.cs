using System.Collections;
using UnityEngine;

public class UpgradeZoneIndicator : Indicator
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _hideDistance;
    [SerializeField] private Wallet _wallet;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _wallet.MoneyAmountChanged += OnMoneyAmountChanged;
    }

    private void OnDisable()
    {
        _wallet.MoneyAmountChanged -= OnMoneyAmountChanged;
    }

    private void OnMoneyAmountChanged(int amount)
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
        while (_wallet.Money > 0)
        {
            ShowIndicator(_target.position, _hideDistance);
            yield return null;
        }
    }
}