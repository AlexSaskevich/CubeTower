using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] private Collector _collector;
    [SerializeField] private WinBannerAnimation _winBannerAnimation;
    [SerializeField] private NextButtonAnimation _nextButtonAnimation;

    private void OnEnable()
    {
        _collector.AllCollected += OnAllCollected;
    }

    private void OnDisable()
    {
        _collector.AllCollected -= OnAllCollected;
    }

    private void OnAllCollected()
    {
        _winBannerAnimation.StartAnimation();
    }

    private void StartButtonAnimation()
    {
        _nextButtonAnimation.StartAnimation();
    }
}