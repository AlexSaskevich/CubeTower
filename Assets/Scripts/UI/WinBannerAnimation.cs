using UnityEngine;

public class WinBannerAnimation : MonoBehaviour
{
    private const string Won = "Won";

    [SerializeField] private Animator _winBannerAnimator;

    public void StartAnimation()
    {
        _winBannerAnimator.SetTrigger(Won);
    }
}