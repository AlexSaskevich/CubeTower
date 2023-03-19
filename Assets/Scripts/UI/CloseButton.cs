using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(IViewable))]
public class CloseButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private SoundButton _soundButton;

    private IViewable _view;

    private void Awake()
    {
        _view = GetComponent<IViewable>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnCloseButtonClick);
        _soundButton.SoundSettingsChanged += OnSoundSettingsChanged;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnCloseButtonClick);
        _soundButton.SoundSettingsChanged -= OnSoundSettingsChanged;
    }

    private void OnCloseButtonClick()
    {
        if (_audioSource.enabled)
            _audioSource.Play();

        _view.Hide();
    }

    private void OnSoundSettingsChanged(bool isPlaying)
    {
        _audioSource.enabled = isPlaying;
    }
}