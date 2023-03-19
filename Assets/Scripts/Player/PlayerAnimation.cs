using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private const string IsRunning = "IsRunning";
    private const string IsSliding = "IsSliding";

    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Collector _collector;
    [SerializeField] private CubeRemover _cubeRemover;

    private void OnEnable()
    {
        _playerMovement.Moved += OnMoved;
        _playerMovement.Stopped += OnStopped;
        _collector.CubeCollected += OnCubecollected;
        _cubeRemover.AllCubesRemoved += OnAllCubesRemoved;
    }

    private void OnDisable()
    {
        _playerMovement.Moved -= OnMoved;
        _playerMovement.Stopped -= OnStopped;
        _collector.CubeCollected -= OnCubecollected;
        _cubeRemover.AllCubesRemoved -= OnAllCubesRemoved;
    }

    public void Jump(float cubeSize)
    {
        Vector3 newPosition = new(0, cubeSize, 0);
        Vector3 targetPosition = transform.localPosition + newPosition;
        transform.localPosition = targetPosition;
    }

    public void Fall(float cubeSize)
    {
        Vector3 newPosition = new(0, cubeSize, 0);
        Vector3 targetposition = transform.localPosition - newPosition;
        transform.localPosition = targetposition;
    }

    private void OnCubecollected(Cube cube)
    {
        _animator.SetBool(IsRunning, false);
        _animator.SetBool(IsSliding, true);
    }

    private void OnAllCubesRemoved()
    {
        _animator.SetBool(IsSliding, false);
    }

    private void OnMoved()
    {
        _animator.SetBool(IsRunning, true);
    }

    private void OnStopped()
    {
        _animator.SetBool(IsRunning, false);
    }
}