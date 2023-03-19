using Agava.WebUtility;
using UnityEngine;

public class SoundMuter : MonoBehaviour
{
    private void OnEnable()
    {
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
    }

    private void OnDisable()
    {
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
    }

    private void OnApplicationFocus(bool isFocus)
    {
        Pause(!isFocus);
    }

    private void OnApplicationPause(bool isPause)
    {
        Pause(isPause);
    }

    public void Mute()
    {
        Pause(true);
    }

    public void Play()
    {
        Pause(false);
    }

    private void Pause(bool isPause)
    {
        AudioListener.pause = isPause;
        AudioListener.volume = isPause ? 0f : 1f;
    }

    private void OnInBackgroundChange(bool inBackground)
    {
        Pause(inBackground);
    }
}