using UnityEngine;

public class AvailableCubeSizeView : MonoBehaviour
{
    [SerializeField] private Collector _collector;

    private float _size;

    private void Awake()
    {
        _size = _collector.AvailableCubeSize;
        transform.localScale = new Vector3(_size, _size, _size);
    }

    private void OnEnable()
    {
        _collector.AvailableCubeSizeChanged += OnAvailableCubeSizeChanged;
    }

    private void OnDisable()
    {
        _collector.AvailableCubeSizeChanged -= OnAvailableCubeSizeChanged;
    }

    private void OnAvailableCubeSizeChanged()
    {
        transform.localScale = new Vector3(_collector.AvailableCubeSize, _collector.AvailableCubeSize, _collector.AvailableCubeSize);
    }
}