using TMPro;
using UnityEngine;

public class LeaderboardRow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _rank;
    [SerializeField] private TextMeshProUGUI _nickname;
    [SerializeField] private TextMeshProUGUI _score;

    public void Set(int rank, string nickname, int score)
    {
        _rank.text = rank.ToString();
        _nickname.text = nickname;
        _score.text = AbbreviationUutility.ConvertMoney(score);
    }
}