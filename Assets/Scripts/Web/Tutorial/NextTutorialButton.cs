using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Animator))]
public class NextTutorialButton : MonoBehaviour
{
    private const string Press = "Press";
    private const float MinPitch = 0.9f;
    private const float MaxPitch = 1.1f;

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
        {
            _audioSource.pitch = Random.Range(MinPitch, MaxPitch);
            _audioSource.Play();
        }

        _animator.SetTrigger(Press);
    }

    private void OnSoundSettingsChanged(bool isPlaying)
    {
        _audioSource.enabled = isPlaying;
    }
}