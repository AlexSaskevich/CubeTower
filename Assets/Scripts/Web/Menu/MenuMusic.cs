using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MenuMusic : MonoBehaviour
{
    [SerializeField] private MusicButton _musicButton;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _musicButton.MusicSettingChanged += OnMusicSettingChanged;
    }

    private void OnDisable()
    {
        _musicButton.MusicSettingChanged -= OnMusicSettingChanged;
    }

    private void OnMusicSettingChanged(bool isPlaying)
    {
        _audioSource.enabled = isPlaying;

        if (isPlaying)
            _audioSource.Play();
        else
            _audioSource.Stop();
    }
}