using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ConveyorAnimation : MonoBehaviour
{
    private const string Pop = "Pop";

    [SerializeField] private CubeRemover _cubeRemover;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
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
        _animator.SetTrigger(Pop);
    }
}