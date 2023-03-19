using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class СonveyorAudio : MonoBehaviour
{
    private const float MinPitch = 1.1f;
    private const float MaxPitch = 1.3f;

    [SerializeField] private CubeRemover _cubeRemover;
    [SerializeField] private float _volume;
    [SerializeField] private SoundButton _soundButton;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _cubeRemover.CubeRemoved += OnCubeRemoved;
        _soundButton.SoundSettingsChanged += OnSoundSettingsChanged;
    }

    private void OnDisable()
    {
        _cubeRemover.CubeRemoved -= OnCubeRemoved;
        _soundButton.SoundSettingsChanged -= OnSoundSettingsChanged;
    }

    private void OnCubeRemoved(Cube cube)
    {
        if (_audioSource.enabled == false)
            return;

        PlaySound();
    }

    private void PlaySound()
    {
        _audioSource.pitch = Random.Range(MinPitch, MaxPitch);
        _audioSource.PlayOneShot(_audioSource.clip, _volume);
    }

    private void OnSoundSettingsChanged(bool isPlaying)
    {
        _audioSource.enabled = isPlaying;
    }
}