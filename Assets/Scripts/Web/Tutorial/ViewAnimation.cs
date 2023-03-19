using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class ViewAnimation : MonoBehaviour
{
    [SerializeField] private float _scaleDuration;

    private GameObject _gameObject;
    private Coroutine _showCoroutine;
    private Coroutine _hideCoroutine;
    private Vector3 _targetScale;
    private Vector3 _startScale;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _gameObject = gameObject;
        _startScale = new Vector3(0.01f, 0.01f, 0.01f);
        _gameObject.transform.localScale = _startScale;
        _targetScale = Vector3.one;
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
    }

    public void StartShow()
    {
        if (_showCoroutine != null)
            StopCoroutine(Scale(_targetScale));

        _canvasGroup.alpha = 1f;
        _showCoroutine = StartCoroutine(Scale(_targetScale));
    }

    public void StartHide()
    {
        if (_hideCoroutine != null)
            StopCoroutine(Scale(_startScale));

        _hideCoroutine = StartCoroutine(Scale(_startScale));
    }

    private IEnumerator Scale(Vector3 targetScale)
    {
        float time = 0;

        while (time < _scaleDuration)
        {
            _gameObject.transform.localScale = Vector3.Lerp(_gameObject.transform.localScale, targetScale, time / _scaleDuration);
            time += Time.deltaTime;
            yield return null;
        }

        _gameObject.transform.localScale = targetScale;

        _canvasGroup.alpha = targetScale == _startScale ? 0 : 1;
    }
}