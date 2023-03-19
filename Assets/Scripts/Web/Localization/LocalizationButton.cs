using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Animator))]
public class LocalizationButton : MonoBehaviour, IButton
{
    private const string Press = "Press";

    [SerializeField] private LanguageView _languageView;
    [SerializeField] private List<AudioSource> _audioSources;
    [SerializeField] private SoundButton _soundButton;

    private Button _button;
    private Animator _animator;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnLanguageButtonClick);
        _soundButton.SoundSettingsChanged += OnSoundSettingsChanged;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnLanguageButtonClick);
        _soundButton.SoundSettingsChanged -= OnSoundSettingsChanged;
    }

    public void Show()
    {
        _button.interactable = true;
    }

    public void Hide()
    {
        _button.interactable = false;
    }

    private void OnLanguageButtonClick()
    {
        _animator.SetTrigger(Press);

        if (_languageView.IsVisible)
            _languageView.Hide();
        else
            _languageView.Show();

        if (_audioSources.Any(a => a.enabled))
            _audioSources[Convert.ToInt32(_languageView.IsVisible)].Play();
    }

    private void OnSoundSettingsChanged(bool isPlaying)
    {
        foreach (var audioSource in _audioSources)
            audioSource.enabled = isPlaying;
    }
}