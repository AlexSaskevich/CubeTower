using Agava.YandexGames;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Animator))]
public class LeaderboardButton : MonoBehaviour, IButton
{
    private const string Press = "Press";

    [SerializeField] private Button _button;
    [SerializeField] private LeaderboardView _leaderboardView;
    [SerializeField] private List<AudioSource> _audioSources;
    [SerializeField] private SoundButton _soundButton;

    private CanvasGroup _canvasGroup;
    private Animator _animator;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnLeaderboardButtonClick);
        _soundButton.SoundSettingsChanged += OnSoundSettingsChanged;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnLeaderboardButtonClick);
        _soundButton.SoundSettingsChanged -= OnSoundSettingsChanged;
    }

    public void Show()
    {
        _canvasGroup.alpha = 1;
        _button.interactable = true;
    }

    public void Hide()
    {
        _canvasGroup.alpha = 0;
        _button.interactable = false;
    }

    private void OnLeaderboardButtonClick()
    {
        _animator.SetTrigger(Press);

        switch (PlayerAccount.IsAuthorized)
        {
            case false when PlayerAccount.HasPersonalProfileDataPermission == false:
                PlayerAccount.Authorize(onSuccessCallback: RequestPersonalData);
                break;
            case true when PlayerAccount.HasPersonalProfileDataPermission:
                ShowLeaderboard();
                break;
            case true when PlayerAccount.HasPersonalProfileDataPermission == false:
                RequestPersonalData();
                break;
        }
    }

    private void ShowLeaderboard()
    {
        if (_leaderboardView.IsVisible)
            _leaderboardView.Hide();
        else
            _leaderboardView.Show();

        if (_audioSources.Any(a => a.enabled))
            _audioSources[Convert.ToInt32(_leaderboardView.IsVisible)].Play();
    }

    private void ShowLeaderboard(string errorCallback = null)
    {
        if (_leaderboardView.IsVisible)
            _leaderboardView.Hide();
        else
            _leaderboardView.Show();

        if (_audioSources.Any(a => a.enabled))
            _audioSources[Convert.ToInt32(_leaderboardView.IsVisible)].Play();
    }

    private void OnSoundSettingsChanged(bool isPlaying)
    {
        foreach (var audioSource in _audioSources)
            audioSource.enabled = isPlaying;
    }

    private void RequestPersonalData()
    {
        PlayerAccount.RequestPersonalProfileDataPermission(onSuccessCallback: ShowLeaderboard, onErrorCallback: ShowLeaderboard);
    }
}