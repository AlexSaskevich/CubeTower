using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Button))]
public class ZoomButton : MonoBehaviour, IButton
{
    private const string Press = "Press";

    [SerializeField] private SoundButton _soundButton;
    [SerializeField] private AudioSource _audioSource;

    private Animator _animator;
    private Button _button;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _animator = GetComponent<Animator>();
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnZoomButtonClick);
        _soundButton.SoundSettingsChanged += OnSoundSettingsChanged;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnZoomButtonClick);
        _soundButton.SoundSettingsChanged -= OnSoundSettingsChanged;
    }

    public void Show()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
    }

    public void Hide()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
    }

    private void OnZoomButtonClick()
    {
        if (_audioSource.enabled)
            _audioSource.Play();

        _animator.SetTrigger(Press);
    }

    private void OnSoundSettingsChanged(bool isPlaying)
    {
        _audioSource.enabled = isPlaying;
    }
}