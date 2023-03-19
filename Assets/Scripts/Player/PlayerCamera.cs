using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerCamera : MonoBehaviour
{
    private const float Delta = 0.1f;

    [SerializeField] private Transform _player;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _speed;
    [SerializeField] private CubeRemover _cubeRemover;
    [SerializeField] private Collector _collector;
    [SerializeField] private Button _inButton;
    [SerializeField] private Button _outButton;

    private Coroutine _zoomInCoroutine;
    private Coroutine _zoomOutCoroutine;
    private Vector3 _startOffset;

    public event UnityAction<Vector3> ZoomedIn;
    public event UnityAction<Vector3> ZoomedOut;

    private void OnEnable()
    {
        _collector.CubeCollected += OnCubeCollected;
        _cubeRemover.CubeRemoved += OnCubeRemoved;
        _inButton.onClick.AddListener(OnZoomInButtonClick);
        _outButton.onClick.AddListener(OnZoomOutButtonClick);
    }

    private void OnDisable()
    {
        _collector.CubeCollected -= OnCubeCollected;
        _cubeRemover.CubeRemoved -= OnCubeRemoved;
        _inButton.onClick.RemoveListener(OnZoomInButtonClick);
        _outButton.onClick.RemoveListener(OnZoomOutButtonClick);
    }

    private void Start()
    {
        _startOffset = _offset;
        _outButton.interactable = false;
        _zoomOutCoroutine = StartCoroutine(ZoomOut());
    }

    private void OnZoomInButtonClick()
    {
        _inButton.interactable = false;
        _outButton.interactable = true;

        if (_zoomOutCoroutine != null)
            StopCoroutine(_zoomOutCoroutine);

        _zoomInCoroutine = StartCoroutine(ZoomIn());
        ZoomedIn?.Invoke(_collector.transform.position + _startOffset);
    }

    private void OnZoomOutButtonClick()
    {
        _outButton.interactable = false;
        _inButton.interactable = true;

        if (_zoomInCoroutine != null)
            StopCoroutine(_zoomInCoroutine);

        _zoomOutCoroutine = StartCoroutine(ZoomOut());
        ZoomedOut?.Invoke(_player.position + _offset);
    }

    private IEnumerator ZoomIn()
    {
        while (true)
        {
            Vector3 desiredPosition = _collector.transform.position + _startOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _speed * Time.deltaTime);

            transform.position = smoothedPosition;
            yield return null;
        }
    }

    private IEnumerator ZoomOut()
    {
        while (true)
        {
            Vector3 desiredPosition = _player.position + _offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _speed * Time.deltaTime);

            transform.position = smoothedPosition;
            yield return null;
        }
    }

    private void OnCubeCollected(Cube cube)
    {
        _offset += GetCameraOffset(cube.Size);
    }

    private void OnCubeRemoved(Cube cube)
    {
        _offset -= GetCameraOffset(cube.Size);
    }

    private Vector3 GetCameraOffset(float cubeSize)
    {
        return cubeSize switch
        {
            CubeSize.XS => new Vector3(0, CubeSize.XS / 2, -CubeSize.XS + Delta),
            CubeSize.S => new Vector3(0, CubeSize.S / 2, -CubeSize.S + Delta),
            CubeSize.M => new Vector3(0, CubeSize.M / 2, -CubeSize.M + Delta),
            CubeSize.L => new Vector3(0, CubeSize.L / 2, -CubeSize.L + Delta),
            CubeSize.XL => new Vector3(0, CubeSize.XL / 2, -CubeSize.XL + Delta),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}