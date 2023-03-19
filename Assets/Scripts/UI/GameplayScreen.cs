using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class GameplayScreen : MonoBehaviour
{
    [SerializeField] private Collector _collector;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

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
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
    }
}