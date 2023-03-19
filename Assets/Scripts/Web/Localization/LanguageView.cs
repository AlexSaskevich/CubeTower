using UnityEngine;

[RequireComponent(typeof(ViewAnimation))]
public class LanguageView : MonoBehaviour, IViewable
{
    [SerializeField] private LeaderboardView _leaderboardView;

    private ViewAnimation _viewAnimation;

    public bool IsVisible { get; private set; }

    private void Awake()
    {
        IsVisible = false;
        _viewAnimation = GetComponent<ViewAnimation>();
    }

    public void Show()
    {
        if (_leaderboardView.IsVisible)
            _leaderboardView.Hide();

        IsVisible = true;
        _viewAnimation.StartShow();
    }

    public void Hide()
    {
        IsVisible = false;
        _viewAnimation.StartHide();
    }
}