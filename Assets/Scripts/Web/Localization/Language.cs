using Lean.Localization;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Language : MonoBehaviour
{
    private const float MinPitch = 0.9f;
    private const float MaxPitch = 1.1f;

    [SerializeField] private List<GameObject> _languageButtons;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private SoundButton _soundButton;

    public event UnityAction<string> LanguageChanged;

    private void OnEnable()
    {
        foreach (var languageButton in _languageButtons)
            languageButton.GetComponent<Button>().onClick.AddListener(OnLanguageButtonClicked);

        _soundButton.SoundSettingsChanged += OnSoundSettingsChanged;
    }

    private void OnDisable()
    {
        foreach (var languageButton in _languageButtons)
            languageButton.GetComponent<Button>().onClick.RemoveListener(OnLanguageButtonClicked);

        _soundButton.SoundSettingsChanged -= OnSoundSettingsChanged;
    }

    public void OnLanguageButtonClicked()
    {
        if (_audioSource.enabled)
        {
            _audioSource.pitch = Random.Range(MinPitch, MaxPitch);
            _audioSource.Play();
        }

        ILanguageButton languageButton = EventSystem.current.currentSelectedGameObject.GetComponent<ILanguageButton>();

        LeanLocalization.SetCurrentLanguageAll(languageButton.Language);
        LanguageChanged?.Invoke(languageButton.Language);
    }

    private void OnSoundSettingsChanged(bool isPlaying)
    {
        _audioSource.enabled = isPlaying;
    }
}