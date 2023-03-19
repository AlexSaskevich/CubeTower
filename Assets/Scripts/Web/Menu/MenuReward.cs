using Agava.YandexGames;
using UnityEngine;

public class MenuReward : MonoBehaviour
{
    [SerializeField] private int _rewardAmount;
    [SerializeField] private SquareContainer _squareContainer;
    [SerializeField] private AdvertisementHandler _advertisingHandler;
    [SerializeField] private MenuRewardView _menuRewardView;

    private void OnEnable()
    {
        _squareContainer.ContainerCleared += OnContainerCleared;
    }

    private void OnDisable()
    {
        _squareContainer.ContainerCleared -= OnContainerCleared;
    }

    private void OnContainerCleared()
    {
        VideoAd.Show(OnOpenCallback, OnRewardedCallBack, OnCloseCallback, OnErrorCallback);
    }

    private void OnOpenCallback()
    {
        _advertisingHandler.MuteSound();
        _advertisingHandler.PauseGame();
    }

    private void OnRewardedCallBack()
    {
        _advertisingHandler.GiveReward(_rewardAmount);
    }

    private void OnCloseCallback()
    {
        _advertisingHandler.PlaySound();
        _advertisingHandler.ContinueGame();
        _menuRewardView.Show();
    }

    private void OnErrorCallback(string errorMessage)
    {
        _advertisingHandler.ContinueGame();
    }
}