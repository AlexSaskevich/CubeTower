using UnityEngine;

public class AdvertisementHandler : MonoBehaviour
{
    [SerializeField] private SoundMuter _soundMuter;
    [SerializeField] private Wallet _wallet;

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
    }

    public void MuteSound()
    {
        _soundMuter.Mute();
    }

    public void PlaySound()
    {
        _soundMuter.Play();
    }

    public void GiveReward(int rewardAmount)
    {
        _wallet.AddReward(rewardAmount);
    }
}