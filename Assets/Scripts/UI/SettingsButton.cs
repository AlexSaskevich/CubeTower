using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CanvasGroup))]
public class SettingsButton : MonoBehaviour, IButton
{
    private const string IsShow = "IsShow";
    private const string IsHide = "IsHide";

    [SerializeField] private Button _button;
    [SerializeField] private List<AudioSource> _audioSources;
    [SerializeField] private SoundButton _soundButton;

    private Animator _animator;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnSettingsButtonClick);
        _soundButton.SoundSettingsChanged += OnSoundSettingsChanged;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnSettingsButtonClick);
        _soundButton.SoundSettingsChanged -= OnSoundSettingsChanged;
    }

    public void Show()
    {
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.alpha = 1;
        _button.interactable = true;
    }

    public void Hide()
    {
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.alpha = 0;
        _button.interactable = false;
    }

    private void OnSettingsButtonClick()
    {
        if (_animator.GetBool(IsShow))
        {
            _animator.SetBool(IsHide, true);
            _animator.SetBool(IsShow, false);
        }
        else
        {
            _animator.SetBool(IsHide, false);
            _animator.SetBool(IsShow, true);
        }

        if (_audioSources.Any(a => a.enabled))
            _audioSources[Convert.ToInt32(_animator.GetBool(IsShow))].Play();
    }

    private void OnSoundSettingsChanged(bool isPlaying)
    {
        foreach (var audioSource in _audioSources)
            audioSource.enabled = isPlaying;
    }
}