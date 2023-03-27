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

    private const string SavedCurrentMoney = "SavedCurrentMoney";
    private const string SavedTotalMoney = "SavedTotalMoney";
    private const string SavedLevel = "SavedLevel";
    private const string SavedTutorialProgress = "SavedTutorialProgress";
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
        else
        {
            PlayerPrefs.SetInt(SavedTotalMoney, totalMoney);
            PlayerPrefs.Save();
        }
    }

    public void SaveCurrentMoney(int money)
    {
        SaveData.CurrentMoney = money;

        if (PlayerAccount.IsAuthorized)
            PlayerAccount.SetPlayerData(JsonUtility.ToJson(SaveData));
        else
        {
            PlayerPrefs.SetInt(SavedCurrentMoney, money);
            PlayerPrefs.Save();
        }
    }

    public void SaveLevel()
    {
        SaveData.Level = SceneManager.GetActiveScene().name;

        if (PlayerAccount.IsAuthorized)
            PlayerAccount.SetPlayerData(JsonUtility.ToJson(SaveData));
        else
        {
            PlayerPrefs.SetString(SavedLevel, SaveData.Level);
            PlayerPrefs.Save();
        }
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
        else
        {
            PlayerPrefs.SetString(SavedTutorialProgress, isTutorialComplete.ToString());
            PlayerPrefs.Save();
        }
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
        else
        {
            SaveData.CurrentMoney = PlayerPrefs.GetInt(SavedCurrentMoney);
            SaveData.Level = PlayerPrefs.GetString(SavedLevel);
            SaveData.TotalMoney = PlayerPrefs.GetInt(SavedTotalMoney);
            string savedTutorialProgress = PlayerPrefs.GetString(SavedTutorialProgress);
            SaveData.IsTutorialComplete = string.IsNullOrEmpty(savedTutorialProgress) ? false : bool.Parse(savedTutorialProgress);
        }
    }
}