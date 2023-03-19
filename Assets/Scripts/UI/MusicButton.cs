using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Animator))]
public class MusicButton : MonoBehaviour, IButton
{
    private const string Press = "Press";

    [SerializeField] private Image _image;
    [SerializeField] private SoundButton _soundButton;
    [SerializeField] private List<AudioSource> _audioSources;
    [SerializeField] private LevelAdvertisement _levelAdvertisement;

    private Button _button;
    private bool _isPlaying;
    private Animator _animator;

    public event UnityAction<bool> MusicSettingChanged;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == LevelName.MENU)
            LoadMusic();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnMusicButtonClick);
        _soundButton.SoundSettingsChanged += OnSoundSettingsChanged;
        _levelAdvertisement.AdvertisementClosed += OnAdvertisementClosed;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnMusicButtonClick);
        _soundButton.SoundSettingsChanged -= OnSoundSettingsChanged;
        _levelAdvertisement.AdvertisementClosed -= OnAdvertisementClosed;
    }

    public void Show()
    {
        _button.interactable = true;
    }

    public void Hide()
    {
        _button.interactable = false;
    }

    private void SetMusic(bool isPlaying)
    {
        _isPlaying = isPlaying;
        _image.enabled = !isPlaying;
        MusicSettingChanged?.Invoke(_isPlaying);
    }

    private void OnMusicButtonClick()
    {
        if (_audioSources.Any(a => a.enabled))
            _audioSources[Convert.ToInt32(_isPlaying)].Play();

        _animator.SetTrigger(Press);
        SetMusic(!_isPlaying);
        Saver.Instance.SaveMusic(_isPlaying);
    }

    private void OnSoundSettingsChanged(bool isPlaying)
    {
        foreach (var audioSource in _audioSources)
            audioSource.enabled = isPlaying;
    }

    private void OnAdvertisementClosed()
    {
        LoadMusic();
    }

    private void LoadMusic()
    {
        string savedMusic = Saver.Instance.LoadMusic();

        _isPlaying = string.IsNullOrEmpty(savedMusic) || bool.Parse(savedMusic);

        SetMusic(_isPlaying);
    }
}