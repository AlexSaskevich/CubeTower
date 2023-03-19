using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ViewAnimation))]
public class MenuRewardView : MonoBehaviour, IViewable
{
    [SerializeField] private LeaderboardView _leaderboardView;
    [SerializeField] private LeaderboardButton _leaderboardButton;
    [SerializeField] private LanguageView _languageView;
    [SerializeField] private SettingsButton _settingsButton;
    [SerializeField] private SoundButton _soundButton;
    [SerializeField] private List<AudioSource> _audioSources;

    private ViewAnimation _viewAnimation;
    private bool _isVisible;

    private void Awake()
    {
        _viewAnimation = GetComponent<ViewAnimation>();
        _isVisible = false;
    }

    private void OnEnable()
    {
        _soundButton.SoundSettingsChanged += OnSoundButtonChanged;
    }

    private void OnDisable()
    {
        _soundButton.SoundSettingsChanged -= OnSoundButtonChanged;
    }

    public void Show()
    {
        if (_languageView.IsVisible)
            _languageView.Hide();

        if (_leaderboardView.IsVisible)
            _leaderboardView.Hide();

        _leaderboardButton.Hide();
        _settingsButton.Hide();

        _viewAnimation.StartShow();
        _isVisible = true;

        if (_audioSources.Any(a => a.enabled))
            _audioSources[Convert.ToInt32(_isVisible)].Play();
    }

    public void Hide()
    {
        _viewAnimation.StartHide();
        _isVisible = false;

        _leaderboardButton.Show();
        _settingsButton.Show();

        if (_audioSources.Any(a => a.enabled))
            _audioSources[Convert.ToInt32(_isVisible)].Play();
    }

    private void OnSoundButtonChanged(bool isPlaying)
    {
        foreach (AudioSource audioSource in _audioSources)
            audioSource.enabled = isPlaying;
    }
}