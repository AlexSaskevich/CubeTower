using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerAudio : MonoBehaviour
{
    private const float MinPitch = 0.9f;
    private const float MaxPitch = 1.1f;

    [SerializeField] private Collector _collector;
    [SerializeField] private SoundButton _soundButton;
    [SerializeField] private AudioSource _dollarCollectSound;
    [SerializeField] private AudioSource _dollarCollectSound1;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _collector.CubeCollected += OnCubeCollected;
        _collector.DollarCollected += OnDollarCollected;
        _soundButton.SoundSettingsChanged += OnSoundSettingsChanged;
    }

    private void OnDisable()
    {
        _collector.CubeCollected -= OnCubeCollected;
        _collector.DollarCollected -= OnDollarCollected;
        _soundButton.SoundSettingsChanged -= OnSoundSettingsChanged;
    }

    private void OnCubeCollected(Cube cube)
    {
        if (_audioSource.enabled == false)
            return;

        PlaySound();
    }

    private void OnDollarCollected(Dollar dollar)
    {
        if (_audioSource.enabled == false)
            return;

        if (_dollarCollectSound.isPlaying)
            _dollarCollectSound1.Play();
        else
            _dollarCollectSound.Play();
    }

    private void PlaySound()
    {
        _audioSource.pitch = Random.Range(MinPitch, MaxPitch);
        _audioSource.PlayOneShot(_audioSource.clip);
    }

    private void OnSoundSettingsChanged(bool isPlaying)
    {
        _audioSource.enabled = isPlaying;
    }
}