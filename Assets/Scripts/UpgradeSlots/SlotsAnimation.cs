using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class SlotsAnimation : MonoBehaviour
{
    private const string IsHide = "IsHide";
    private const string IsShow = "IsShow";

    private Animator _animator;

    public event UnityAction Finished;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Show()
    {
        _animator.SetBool(IsHide, false);
        _animator.SetBool(IsShow, true);
    }

    public void Hide()
    {
        _animator.SetBool(IsHide, true);
        _animator.SetBool(IsShow, false);
    }

    public void OnHideAnimationFinished()
    {
        Finished?.Invoke();
    }
}