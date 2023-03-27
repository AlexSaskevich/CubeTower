using Agava.WebUtility;
using UnityEngine;

public class SoundMuter : MonoBehaviour
{
    private bool _isAdvertisementShowing;

    private void OnEnable()
    {
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
    }

    private void OnDisable()
    {
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
    }

    public void Mute()
    {
        Pause(true);
        _isAdvertisementShowing = true;
    }

    public void Play()
    {
        Pause(false);
        _isAdvertisementShowing = false;
    }

    private void Pause(bool isPause)
    {
        AudioListener.pause = isPause;
        AudioListener.volume = isPause ? 0f : 1f;
    }

    private void OnInBackgroundChange(bool inBackground)
    {
        if (_isAdvertisementShowing)
            return;

        Pause(inBackground);
    }
}