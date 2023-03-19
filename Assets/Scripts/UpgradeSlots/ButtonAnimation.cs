using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Button))]
public class ButtonAnimation : MonoBehaviour
{
    private const string Pressed = "Pressed";

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private SoundButton _soundButton;

    private Animator _animator;
    private Button _button;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(PlayPressAnimation);
        _soundButton.SoundSettingsChanged += OnSoundSettingsChanged;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(PlayPressAnimation);
        _soundButton.SoundSettingsChanged -= OnSoundSettingsChanged;
    }

    public void SetAnimation(int playerMoney, int upgradePrice)
    {
        _button.interactable = playerMoney >= upgradePrice;
    }

    private void PlayPressAnimation()
    {
        if (_audioSource.enabled)
            _audioSource.Play();

        _audioSource.pitch += 0.02f;

        _animator.SetTrigger(Pressed);
    }

    private void OnSoundSettingsChanged(bool isPlaying)
    {
        _audioSource.enabled = isPlaying;
    }
}