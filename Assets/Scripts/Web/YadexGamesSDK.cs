using Agava.YandexGames;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YadexGamesSDK : MonoBehaviour
{
    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
        yield return YandexGamesSdk.Initialize(onSuccessCallback: LoadMenu);
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene(LevelName.MENU);
    }
}