using UnityEngine;

[RequireComponent(typeof(CubeAnimation))]
public class CubeVFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem _cloudsParticleSystem;

    private CubeAnimation _cubeAnimation;

    private void Awake()
    {
        _cubeAnimation = GetComponent<CubeAnimation>();
    }

    private void OnEnable()
    {
        _cubeAnimation.Throwed += OnThrowed;
    }

    private void OnDisable()
    {
        _cubeAnimation.Throwed -= OnThrowed;
    }

    private void OnThrowed()
    {
        _cloudsParticleSystem.Play();
    }
}