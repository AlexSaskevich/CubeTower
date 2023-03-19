using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DollarIconAnimation : MonoBehaviour
{
    [SerializeField] private float _moveDuration;
    [SerializeField] private float _scaleDuration;
    [SerializeField] private Transform _target;

    private readonly Vector3 _targetScale = new(0.9f, 0.9f, 0.9f);
    private Image _image;
    private Coroutine _moveCoroutine;
    private Coroutine _scaleCoroutine;
    private Vector3 _originalScale;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _originalScale = _image.transform.localScale;
    }

    public void StartMove()
    {
        if (_moveCoroutine != null)
            StopCoroutine(Move());

        _moveCoroutine = StartCoroutine(Move());
    }

    public void StartScale()
    {
        if (_scaleCoroutine != null)
            StopCoroutine(Scale());

        _scaleCoroutine = StartCoroutine(Scale());
    }

    private IEnumerator Move()
    {
        float time = 0;

        while (time < _moveDuration)
        {
            _image.transform.position = Vector3.Lerp(_image.transform.position, _target.position, time / _moveDuration);
            time += Time.deltaTime;
            yield return null;
        }

        _image.transform.position = _target.position;
    }

    private IEnumerator Scale()
    {
        float time = 0;

        while (time < _scaleDuration)
        {
            _image.transform.localScale = Vector3.Lerp(_image.transform.localScale, _targetScale, time / _scaleDuration);
            time += Time.deltaTime;
            yield return null;
        }

        _image.transform.localScale = _originalScale;
        gameObject.SetActive(false);
    }
}