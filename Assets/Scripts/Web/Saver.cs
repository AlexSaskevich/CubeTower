using Agava.YandexGames;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;

[Serializable]
public class SaveData
{
    [field: Preserve] public int CurrentMoney;
    [field: Preserve] public int TotalMoney;
    [field: Preserve] public string Level;
    [field: Preserve] public bool IsTutorialComplete;
}

public class Saver : MonoBehaviour
{
    public static Saver Instance;

    public SaveData SaveData;

    private const string SavedSound = "SavedSound";
    private const string SavedMusic = "SavedMusic";

    private void Awake()
    {
        if (Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
            Load();
        }
        else
            Destroy(gameObject);
    }

    public void SaveTotalMoney(int totalMoney)
    {
        SaveData.TotalMoney = totalMoney;

        if (PlayerAccount.IsAuthorized)
            PlayerAccount.SetPlayerData(JsonUtility.ToJson(SaveData));
    }

    public void SaveCurrentMoney(int money)
    {
        SaveData.CurrentMoney = money;

        if (PlayerAccount.IsAuthorized)
            PlayerAccount.SetPlayerData(JsonUtility.ToJson(SaveData));
    }

    public void SaveLevel()
    {
        SaveData.Level = SceneManager.GetActiveScene().name;

        if (PlayerAccount.IsAuthorized)
            PlayerAccount.SetPlayerData(JsonUtility.ToJson(SaveData));
    }

    public void SaveSound(bool isPlaying)
    {
        PlayerPrefs.SetString(SavedSound, isPlaying.ToString());
        PlayerPrefs.Save();
    }

    public void SaveMusic(bool isPlaying)
    {
        PlayerPrefs.SetString(SavedMusic, isPlaying.ToString());
        PlayerPrefs.Save();
    }

    public void SaveIsTutorialComplete(bool isTutorialComplete)
    {
        SaveData.IsTutorialComplete = isTutorialComplete;

        if (PlayerAccount.IsAuthorized)
            PlayerAccount.SetPlayerData(JsonUtility.ToJson(SaveData));
    }

    public string LoadMusic()
    {
        return PlayerPrefs.HasKey(SavedMusic) ? PlayerPrefs.GetString(SavedMusic) : null;
    }

    public string LoadSound()
    {
        return PlayerPrefs.HasKey(SavedSound) ? PlayerPrefs.GetString(SavedSound) : null;
    }

    private void Load()
    {
        if (PlayerAccount.IsAuthorized)
            PlayerAccount.GetPlayerData(onSuccessCallback: jsonData => SaveData = JsonUtility.FromJson<SaveData>(jsonData));
    }
}