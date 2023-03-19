using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Animator))]
public class PlayButton : MonoBehaviour
{
    private const string Press = "Press";

    [SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private AudioSource _audioSource;
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
        _button.onClick.AddListener(OnPlayButtonClicked);
        _soundButton.SoundSettingsChanged += OnSoundSettingsChanged;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnPlayButtonClicked);
        _soundButton.SoundSettingsChanged += OnSoundSettingsChanged;
    }

    private void OnPlayButtonClicked()
    {
        if (_audioSource.enabled)
            _audioSource.Play();

        _button.interactable = false;
        _animator.SetTrigger(Press);
        _levelLoader.LoadNextLevel();
    }

    private void OnSoundSettingsChanged(bool isPlaying)
    {
        _audioSource.enabled = isPlaying;
    }
}