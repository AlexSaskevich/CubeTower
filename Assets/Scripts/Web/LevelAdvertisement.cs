using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;

public class LevelAdvertisement : MonoBehaviour
{
    [SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private AdvertisementHandler _advertisingHandler;

    public event UnityAction AdvertisementClosed;

    public void Show()
    {
        InterstitialAd.Show(OnOpenCallback, OnCloseCallback, OnErrorCallback, OnOfflineCallback);
    }

    private void OnOpenCallback()
    {
        _advertisingHandler.MuteSound();
    }

    private void OnCloseCallback(bool isClosed)
    {
        _advertisingHandler.PlaySound();
        AdvertisementClosed?.Invoke();
    }

    private void OnErrorCallback(string errorMessage)
    {
        _advertisingHandler.PlaySound();
        AdvertisementClosed?.Invoke();
    }

    private void OnOfflineCallback()
    {
        _advertisingHandler.PlaySound();
        AdvertisementClosed?.Invoke();
    }
}