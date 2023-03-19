using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Animator))]
public class NextButton : MonoBehaviour
{
    private const string Press = "Press";

    [SerializeField] private SoundButton _soundButton;
    [SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private AudioSource _audioSource;

    private Button _button;
    private CanvasGroup _canvasGroup;
    private Animator _animator;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
        _soundButton.SoundSettingsChanged += OnSoundSettingsChanged;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
        _soundButton.SoundSettingsChanged -= OnSoundSettingsChanged;
    }

    private void OnButtonClick()
    {
        if (_audioSource.enabled)
            _audioSource.Play();

        _animator.SetTrigger(Press);
        _canvasGroup.blocksRaycasts = false;
        _levelLoader.LoadNextLevel();
    }

    private void OnSoundSettingsChanged(bool isPlaying)
    {
        _audioSource.enabled = isPlaying;
    }
}