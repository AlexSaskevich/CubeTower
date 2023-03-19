using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class LevelLoader : MonoBehaviour
{
    private const string Fade = "Fade";

    [SerializeField] private Wallet _wallet;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private LevelAdvertisement _levelAdvertisement;

    private Animator _animator;
    private int _lastSceneIndex;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _canvasGroup.alpha = 1.0f;
        _lastSceneIndex = SceneManager.sceneCountInBuildSettings - 1;
    }

    public void LoadNextLevel()
    {
        _animator.SetTrigger(Fade);
    }

    private void OnFadeOutComplete()
    {
        string savedLevel = Saver.Instance.SaveData.Level;

        if (string.IsNullOrEmpty(savedLevel) || savedLevel == SceneManager.GetActiveScene().name)
            LoadNextScene();
        else
            LoadSavedScene(savedLevel, Saver.Instance.SaveData.CurrentMoney);
    }

    private void OnFadeInComplete()
    {
        if (SceneManager.GetActiveScene().name == LevelName.MENU)
            return;

        _levelAdvertisement.Show();

        Saver.Instance.SaveLevel();
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (SceneManager.GetActiveScene().buildIndex == _lastSceneIndex)
            SceneManager.LoadScene(LevelName.LVL1);
        else
            SceneManager.LoadScene(++currentSceneIndex);

        Saver.Instance.SaveCurrentMoney(_wallet.Money);
    }

    private void LoadSavedScene(string savedLevel, int savedMoney)
    {
        SceneManager.LoadScene(savedLevel);
        Saver.Instance.SaveCurrentMoney(savedMoney);
    }
}