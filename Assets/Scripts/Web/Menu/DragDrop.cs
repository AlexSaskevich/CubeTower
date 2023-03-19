using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
public class DragDrop : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Color _selectedColor;

    private RectTransform _rectTransform;
    private Image _image;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _image.color = _selectedColor;
        SquareContainer.Instance.TryAdd(gameObject);
    }
}