using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private const int MinValue = 0;

    [SerializeField] private CubeRemover _cubeRemover;
    [SerializeField] private Image _image;
    [SerializeField] private LevelConfig _levelConfig;

    private int _currentValue;
    private int _maxValue;

    public event UnityAction Reached;

    private void Awake()
    {
        _image.fillAmount = MinValue;
        _maxValue = _levelConfig.CubeCount;
    }

    private void OnEnable()
    {
        _cubeRemover.CubeRemoved += OnCubeRemoved;
    }

    private void OnDisable()
    {
        _cubeRemover.CubeRemoved -= OnCubeRemoved;
    }

    private void OnCubeRemoved(Cube cube)
    {
        _currentValue++;
        _image.fillAmount = (float)_currentValue / _maxValue;

        if (_currentValue == _maxValue)
            Reached?.Invoke();
    }
}