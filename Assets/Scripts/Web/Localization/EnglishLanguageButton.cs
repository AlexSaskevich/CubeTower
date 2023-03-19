using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Animator))]
public class EnglishLanguageButton : MonoBehaviour, ILanguageButton
{
    private const string English = "English";
    private const string Press = "Press";

    private Button _button;
    private Animator _animator;

    public string Language => English;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        _animator.SetTrigger(Press);
    }
}