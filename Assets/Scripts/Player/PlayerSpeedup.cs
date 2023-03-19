using UnityEngine;
using UnityEngine.Events;

public class PlayerSpeedup : MonoBehaviour
{
    [SerializeField] private float _speedModifier;
    [SerializeField] private Stack _stack;
    [SerializeField] private CubeRemover _cubeRemover;

    public event UnityAction<float> Activated;
    public event UnityAction<float> Deactivated;

    public bool IsActivated { get; private set; }
    public float SpeedModifier => _speedModifier;

    private void OnEnable()
    {
        _stack.Fulled += OnFulled;
        _stack.CapacityChanged += OnCapacityChanged;
        _cubeRemover.CubeRemoved += OnCubeRemoved;
    }

    private void OnDisable()
    {
        _stack.Fulled -= OnFulled;
        _stack.CapacityChanged -= OnCapacityChanged;
        _cubeRemover.CubeRemoved -= OnCubeRemoved;
    }

    private void OnFulled()
    {
        IsActivated = true;
        Activated?.Invoke(_speedModifier);
    }

    private void OnCapacityChanged()
    {
        Deactivate();
    }

    private void OnCubeRemoved(Cube cube)
    {
        Deactivate();
    }

    private void Deactivate()
    {
        IsActivated = false;
        Deactivated?.Invoke(_speedModifier);
    }
}