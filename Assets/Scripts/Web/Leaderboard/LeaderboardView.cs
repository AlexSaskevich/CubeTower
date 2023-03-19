using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ViewAnimation))]
public class LeaderboardView : MonoBehaviour, IViewable
{
    private const string TopPlayers = "TopPlayers";
    private const string Anonymous = "Anonymous";

    [SerializeField] private LanguageView _languageView;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private List<LeaderboardRow> _leaderboardRows;

    private ViewAnimation _viewAnimation;
    private Coroutine _coroutine;

    public bool IsVisible { get; private set; }

    private void Awake()
    {
        IsVisible = false;
        _viewAnimation = GetComponent<ViewAnimation>();
    }

    public void Show()
    {
        if (_languageView.IsVisible)
            _languageView.Hide();

        IsVisible = true;

        StartUpdateLeaderboard();

        _viewAnimation.StartShow();
    }

    public void Hide()
    {
        IsVisible = false;
        _viewAnimation.StartHide();
    }

    private void StartUpdateLeaderboard()
    {
        if (_coroutine != null)
            return;

        _coroutine = StartCoroutine(UpdateLeaderboard());
    }

    private IEnumerator UpdateLeaderboard()
    {
        Leaderboard.GetPlayerEntry(TopPlayers, onSuccessCallback: TryUpdateScore);

        Leaderboard.GetEntries(TopPlayers, result =>
        {
            for (int i = 0; i < result.entries.Length; i++)
            {
                string playerPublicName = result.entries[i].player.publicName;

                if (string.IsNullOrEmpty(playerPublicName))
                    playerPublicName = Anonymous;

                _leaderboardRows[i].Set(result.entries[i].rank, playerPublicName, result.entries[i].score);
            }
        });

        yield return new WaitForSecondsRealtime(3f);
        _coroutine = null;
    }

    private void TryUpdateScore(LeaderboardEntryResponse result)
    {
        if (_wallet.TotalMoney < result.score)
        {
            int loadedTotalMoney = result.score + _wallet.TotalMoney;
            _wallet.SetTotalMoney(loadedTotalMoney);
        }
        else if (_wallet.TotalMoney > result.score)
            Leaderboard.SetScore(TopPlayers, _wallet.TotalMoney);
    }
}