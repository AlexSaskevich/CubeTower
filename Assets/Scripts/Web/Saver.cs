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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            Load();
        else if (Input.GetKeyDown(KeyCode.R))
            ResetData();
    }

    public void SaveTotalMoney(int totalMoney)
    {
        SaveData.TotalMoney = totalMoney;

        if (PlayerAccount.IsAuthorized)
            PlayerAccount.SetPlayerData(JsonUtility.ToJson(SaveData), onSuccessCallback: ShowSaveLogs);
    }

    public void SaveCurrentMoney(int money)
    {
        SaveData.CurrentMoney = money;

        if (PlayerAccount.IsAuthorized)
            PlayerAccount.SetPlayerData(JsonUtility.ToJson(SaveData), onSuccessCallback: ShowSaveLogs);
    }

    public void SaveLevel()
    {
        SaveData.Level = SceneManager.GetActiveScene().name;

        if (PlayerAccount.IsAuthorized)
            PlayerAccount.SetPlayerData(JsonUtility.ToJson(SaveData), onSuccessCallback: ShowSaveLogs);
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
            PlayerAccount.SetPlayerData(JsonUtility.ToJson(SaveData), onSuccessCallback: ShowSaveLogs);
    }

    public string LoadMusic()
    {
        return PlayerPrefs.HasKey(SavedMusic) ? PlayerPrefs.GetString(SavedMusic) : null;
    }

    public string LoadSound()
    {
        return PlayerPrefs.HasKey(SavedSound) ? PlayerPrefs.GetString(SavedSound) : null;
    }

    private void ResetData()
    {
        SaveData.TotalMoney = 0;
        SaveData.CurrentMoney = 0;
        SaveData.Level = "";
        SaveData.IsTutorialComplete = false;

        PlayerPrefs.DeleteAll();

        if (PlayerAccount.IsAuthorized)
            PlayerAccount.SetPlayerData(JsonUtility.ToJson(SaveData));
    }

    private void Load()
    {
        if (PlayerAccount.IsAuthorized)
            PlayerAccount.GetPlayerData(onSuccessCallback: jsonData => ShowLoadLogs(jsonData));
    }

    private void ShowLoadLogs(string jsonData)
    {
        SaveData = JsonUtility.FromJson<SaveData>(jsonData);
        Debug.LogWarning("Вызвался метод Load");
        Debug.LogWarning("загрузка SaveData.TotalMoney = " + SaveData.TotalMoney);
        Debug.LogWarning("загрузка SaveData.CurrentMoney = " + SaveData.CurrentMoney);
        Debug.LogWarning("загрузка SaveData.Level = " + SaveData.Level);
        Debug.LogWarning("загрузка SaveData.IsTutorialComplete = " + SaveData.IsTutorialComplete);
        Debug.LogWarning("загрузка SavedSound = " + PlayerPrefs.GetString(SavedSound));
        Debug.LogWarning("загрузка SavedMusic = " + PlayerPrefs.GetString(SavedMusic));
    }

    private void ShowSaveLogs()
    {
        Debug.LogWarning("сохранение SaveData.TotalMoney = " + SaveData.TotalMoney);
        Debug.LogWarning("сохранение SaveData.CurrentMoney = " + SaveData.CurrentMoney);
        Debug.LogWarning("сохранение SaveData.Level = " + SaveData.Level);
        Debug.LogWarning("сохранение SaveData.IsTutorialComplete = " + SaveData.IsTutorialComplete);
        Debug.LogWarning("сохранение SavedSound = " + PlayerPrefs.GetString(SavedSound));
        Debug.LogWarning("сохранение SavedMusic = " + PlayerPrefs.GetString(SavedMusic));
    }
}