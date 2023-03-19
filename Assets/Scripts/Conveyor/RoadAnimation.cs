using UnityEngine;

public class RoadAnimation : MonoBehaviour
{
    private const string MainTexture = "_MainTex";

    [SerializeField] private float _speedY;
    [SerializeField] private Material _material;

    private float _currentOffsetY;

    private void Awake()
    {
        _material.mainTextureOffset = Vector2.zero;
        _currentOffsetY = _material.mainTextureOffset.y;
    }

    private void Update()
    {
        _currentOffsetY += Time.deltaTime * _speedY;
        _material.SetTextureOffset(MainTexture, new Vector2(0, _currentOffsetY));
    }
}