using Agava.YandexGames;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TutorialView : MonoBehaviour, IViewable
{
    [SerializeField] private ViewAnimation _viewAnimation;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _previousButton;
    [SerializeField] private List<GameObject> _tutorials = new();
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private LevelAdvertisement _levelAdvertisement;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private AudioSource _audioSource;

    private void OnEnable()
    {
        _nextButton.onClick.AddListener(OnNextButtonClick);
        _previousButton.onClick.AddListener(OnPreviousButtonClick);
        _levelAdvertisement.AdvertisementClosed += OnAdvertisementClosed;
    }

    private void OnDisable()
    {
        _nextButton.onClick.RemoveListener(OnNextButtonClick);
        _previousButton.onClick.RemoveListener(OnPreviousButtonClick);
        _levelAdvertisement.AdvertisementClosed -= OnAdvertisementClosed;
    }

    public void Show()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;

        GameObject tutorial = _tutorials?.FirstOrDefault();

        if (tutorial == null)
            return;

        tutorial.SetActive(true);
        _previousButton.gameObject.SetActive(false);

        if (_audioSource.enabled)
            _audioSource.Play();

        _viewAnimation.StartShow();

        _playerMovement.PlayerInput.Disable();
    }

    public void Hide()
    {
        _viewAnimation.StartHide();
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;

        _playerMovement.PlayerInput.Enable();
        Saver.Instance.SaveIsTutorialComplete(true);
    }

    private void OnNextButtonClick()
    {
        _previousButton.gameObject.SetActive(true);

        for (int i = 0; i < _tutorials.Count; i++)
        {
            if (_tutorials[i].activeSelf && _tutorials[i] != _tutorials[^1])
            {
                _tutorials[i].SetActive(false);
                _tutorials[i + 1].SetActive(true);

                if (_tutorials.Last().activeSelf)
                    _nextButton.gameObject.SetActive(false);

                return;
            }
        }
    }

    private void OnPreviousButtonClick()
    {
        _nextButton.gameObject.SetActive(true);

        for (int i = _tutorials.Count - 1; i >= 0; i--)
        {
            if (_tutorials[i].activeSelf && _tutorials[i] != _tutorials.First())
            {
                _tutorials[i].SetActive(false);
                _tutorials[i - 1].SetActive(true);

                if (_tutorials.First().activeSelf)
                    _previousButton.gameObject.SetActive(false);

                return;
            }
        }
    }

    private void OnAdvertisementClosed()
    {
        if (PlayerAccount.IsAuthorized == false || Saver.Instance.SaveData.IsTutorialComplete == false)
            Show();
    }
}