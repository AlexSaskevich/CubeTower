using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Animator))]
public class SoundButton : MonoBehaviour, IButton
{
    private const string Press = "Press";

    [SerializeField] private Image _image;
    [SerializeField] private List<AudioSource> _audioSources;

    private Button _button;
    private bool _isPlaying;
    private Animator _animator;

    public event UnityAction<bool> SoundSettingsChanged;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnSoundButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnSoundButtonClick);
    }

    private void Start()
    {
        string savedSound = Saver.Instance.LoadSound();

        _isPlaying = string.IsNullOrEmpty(savedSound) || bool.Parse(savedSound);

        SetSound(_isPlaying);
    }

    public void Show()
    {
        _button.interactable = true;
    }

    public void Hide()
    {
        _button.interactable = false;
    }

    private void SetSound(bool isPlaying)
    {
        _isPlaying = isPlaying;
        _image.enabled = isPlaying;
        SoundSettingsChanged?.Invoke(_isPlaying);
    }

    private void OnSoundButtonClick()
    {
        _audioSources[Convert.ToInt32(_isPlaying)].Play();

        _animator.SetTrigger(Press);
        SetSound(!_isPlaying);
        Saver.Instance.SaveSound(_isPlaying);
    }
}