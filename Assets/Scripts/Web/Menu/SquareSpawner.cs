using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SquareSpawner : MonoBehaviour
{
    [SerializeField] private int _squareCount;
    [SerializeField] private GameObject _square;
    [SerializeField] private float _radius;
    [SerializeField] private RectTransform _canvas;
    [SerializeField] private List<Color> _colors;

    public int SquareCount => _squareCount;
    public float Radius => _radius;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        for (int i = 0; i < _squareCount; i++)
        {
            GameObject square = Instantiate(_square, GetRandomPosition(), Quaternion.identity, gameObject.transform);
            square.SetActive(true);
            square.GetComponent<Square>().SetColor(_colors[Random.Range(0, _colors.Count)]);
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 position = Random.insideUnitCircle;

        return position * _radius + _canvas.position;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(SquareSpawner))]
public class HandlesTest : Editor
{
    public void OnSceneGUI()
    {
        var linkedObject = target as SquareSpawner;

        Handles.color = Color.green;

        if (linkedObject != null)
            Handles.DrawSolidDisc(linkedObject.transform.position, Vector3.back, linkedObject.Radius);
    }
}
#endif