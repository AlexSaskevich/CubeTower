using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Animator))]
public class StackCapacityView : MonoBehaviour
{
    private const string IsFull = "IsFull";

    [SerializeField] private Stack _stack;
    [SerializeField] private CubeRemover _cubeRemover;
    [SerializeField] private Transform _target;
    [SerializeField] private PlayerCamera _playerCamera;
    [SerializeField] private float _moveDuration;

    private Image _image;
    private Animator _animator;
    private CanvasGroup _canvasGroup;
    private Camera _camera;
    private Vector2 _anchoredPosition;
    private Coroutine _coroutine;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _animator = GetComponent<Animator>();
        _camera = Camera.main;
        _anchoredPosition = new Vector2(0, 1);
    }

    private void OnEnable()
    {
        _stack.Fulled += OnFulled;
        _stack.CapacityChanged += OnCapacityChanged;
        _cubeRemover.CubeRemoved += OnCubeRemoved;
        _playerCamera.ZoomedIn += OnZoomedIn;
        _playerCamera.ZoomedOut += OnZoomedOut;
    }

    private void OnDisable()
    {
        _stack.Fulled -= OnFulled;
        _stack.CapacityChanged -= OnCapacityChanged;
        _cubeRemover.CubeRemoved -= OnCubeRemoved;
        _playerCamera.ZoomedIn -= OnZoomedIn;
        _playerCamera.ZoomedOut -= OnZoomedOut;
    }

    private void OnFulled()
    {
        Vector3 screenPosition = _camera.WorldToScreenPoint(_target.transform.position);
        _image.transform.position = screenPosition;
        _image.rectTransform.anchoredPosition *= _anchoredPosition;
        _canvasGroup.alpha = 1;
        _animator.SetBool(IsFull, true);
    }

    private void OnCubeRemoved(Cube cube)
    {
        SetAnimation();
    }

    private void OnCapacityChanged()
    {
        SetAnimation();
    }

    private void SetAnimation()
    {
        _animator.SetBool(IsFull, _stack.CurrentCapacity == 0);
    }

    private void OnZoomedIn(Vector3 targetPosition)
    {
        StartUpdatePosition();
    }

    private void OnZoomedOut(Vector3 targetPosition)
    {
        StartUpdatePosition();
    }

    private void StartUpdatePosition()
    {
        if (_coroutine != null)
            StopCoroutine(UpdatePosition());

        _coroutine = StartCoroutine(UpdatePosition());
    }

    private IEnumerator UpdatePosition()
    {
        float time = 0;

        while (time < _moveDuration)
        {
            Vector3 screenPosition = _camera.WorldToScreenPoint(_target.transform.position);
            _image.transform.position = Vector3.Lerp(_image.transform.position, screenPosition, time / _moveDuration);
            _image.rectTransform.anchoredPosition *= _anchoredPosition;
            time += Time.deltaTime;
            yield return null;
        }

        _image.transform.position = _camera.WorldToScreenPoint(_target.transform.position);
        _image.rectTransform.anchoredPosition *= _anchoredPosition;
    }
}