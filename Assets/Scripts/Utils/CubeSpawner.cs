using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    private const float PositionY = 0.5f;

    [SerializeField] private float _radius;
    [SerializeField] private int _count;
    [SerializeField] GameObject _prefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InstantiateInCircle();
        }
    }

    private void InstantiateInCircle()
    {
        for (int i = 0; i < _count; i++)
        {
            float angle = i * Mathf.PI * 2f / _count;
            Vector3 newPosition = new Vector3(Mathf.Cos(angle) * _radius, PositionY, Mathf.Sin(angle) * _radius);
            GameObject cube = Instantiate(_prefab, newPosition, Quaternion.identity);
            cube.transform.LookAt(transform.position);
        }
    }
}