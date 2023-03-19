using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Rigidbody2D))]
public class Square : MonoBehaviour
{
    private const float MaxPosition = 3.0f;
    private const float MinPosition = -3.0f;
    private const float MinRotation = 0f;
    private const float MaxRotation = 360f;
    private const float MinTurnAngle = -75f;
    private const float MaxTurnAngle = 75f;

    [SerializeField] private RectTransform _canvas;
    [SerializeField] private float _minSpeed = 15f;
    [SerializeField] private float _maxSpeed = 40f;
    [SerializeField] private float _scaleDuration;

    private Image _image;
    private Rigidbody2D _rigidbody2D;
    private float _speed;
    private float _imageWidth;
    private float _imageHeight;
    private float _randomX;
    private float _randomY;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.simulated = false;
    }

    private void Start()
    {
        _imageWidth = _image.rectTransform.sizeDelta.x / 2f;
        _imageHeight = _image.rectTransform.sizeDelta.y / 2f;
        _randomX = Random.Range(MinPosition, MaxPosition);
        _randomY = Random.Range(MinPosition, MaxPosition);
        _speed = Random.Range(_minSpeed, _maxSpeed);
        _image.rectTransform.Rotate(Vector3.forward * Random.Range(MinRotation, MaxRotation));
    }

    private void Update()
    {
        Move();
    }

    private void LateUpdate()
    {
        ClampPosition();
    }

    public void SetColor(Color color)
    {
        _image.color = color;
    }

    public void StopMove()
    {
        _rigidbody2D.simulated = true;
        enabled = false;
    }

    private void Move()
    {
        _image.rectTransform.Translate(new Vector3(_randomX, _randomY, 0f) * Time.deltaTime * _speed);

        if (_image.rectTransform.anchoredPosition.x >= GetBorderX() || _image.rectTransform.anchoredPosition.x <= -GetBorderX())
            _image.rectTransform.Rotate(Vector3.forward * Random.Range(MinTurnAngle, MaxTurnAngle));

        if (_image.rectTransform.anchoredPosition.y >= GetBorderY() || _image.rectTransform.anchoredPosition.y <= -GetBorderY())
            _image.rectTransform.Rotate(Vector3.forward * Random.Range(MinTurnAngle, MaxTurnAngle));
    }

    private float GetBorderX()
    {
        return _canvas.rect.width / 2f - _imageWidth;
    }

    private float GetBorderY()
    {
        return _canvas.rect.height / 2f - _imageHeight;
    }

    private void ClampPosition()
    {
        var viewPosition = _image.rectTransform.anchoredPosition;
        viewPosition.x = Mathf.Clamp(viewPosition.x, -GetBorderX(), GetBorderX());
        viewPosition.y = Mathf.Clamp(viewPosition.y, -GetBorderY(), GetBorderY());
        _image.rectTransform.anchoredPosition = viewPosition;
    }
}