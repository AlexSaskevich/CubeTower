using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DeathZone : MonoBehaviour
{
    [SerializeField] private RectTransform _canvas;
    [SerializeField] private RectTransform _square;

    private BoxCollider2D _boxCollider2D;

    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        _boxCollider2D.size = new Vector2(_canvas.rect.width, _square.rect.height);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Square square) == false)
            return;

        Destroy(square.gameObject);
    }
}