using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class NextButtonAnimation : MonoBehaviour
{
    [SerializeField] private float _fadeInDuration = 2;

    private CanvasGroup _canvasGroup;
    private Coroutine _coroutine;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0.0f;
        _canvasGroup.blocksRaycasts = false;
    }

    public void StartAnimation()
    {
        if (_coroutine != null)
            StopCoroutine(FadeIn());

        _coroutine = StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float time = 0;

        while (time < _fadeInDuration)
        {
            _canvasGroup.alpha = Mathf.MoveTowards(0, 1, time / _fadeInDuration);
            time += Time.deltaTime;
            yield return null;
        }

        _canvasGroup.blocksRaycasts = true;
    }
}