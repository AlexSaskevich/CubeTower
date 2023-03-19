using UnityEngine;

public class PlayerVFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem _boostParticleSystem;
    [SerializeField] private ParticleSystem _runParticleSystem;
    [SerializeField] private CubeRemover _cubeRemover;
    [SerializeField] private PlayerAnimation _playerAnimation;
    [SerializeField] private Collector _collector;

    private void OnEnable()
    {
        _collector.CubeCollected += OnCubeCollected;
        _cubeRemover.AllCubesRemoved += OnAllCubesRemoved;
    }

    private void OnDisable()
    {
        _collector.CubeCollected -= OnCubeCollected;
        _cubeRemover.AllCubesRemoved -= OnAllCubesRemoved;
    }

    private void OnCubeCollected(Cube cube)
    {
        _boostParticleSystem.Play();
        _runParticleSystem.Stop();
    }

    private void OnAllCubesRemoved()
    {
        _boostParticleSystem.Stop();
        _runParticleSystem.Play();
    }
}