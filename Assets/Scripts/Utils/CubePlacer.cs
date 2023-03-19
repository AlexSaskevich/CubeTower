using UnityEngine;

[ExecuteAlways]
public class CubePlacer : MonoBehaviour
{
    [SerializeField] private int _row = 5;
    [SerializeField] private int _column = 21;
    [SerializeField] private float _stepX;
    [SerializeField] private float _stepZ;

    private GameObject[,] _cubes;

    private void Update()
    {
        _cubes = new GameObject[_row, _column];

        int cubeIndex = 0;

        for (int i = 0; i < _row; i++)
        {
            for (int j = 0; j < _column; j++)
            {
                _cubes[i, j] = transform.GetChild(cubeIndex).gameObject;
                cubeIndex++;
            }
        }

        Arrange();
    }

    private void Arrange()
    {
        Vector3 startPosition = Vector3.zero;
        Vector3 currentPosition = startPosition;

        for (int i = 0; i < _row; i++)
        {
            for (int j = 0; j < _column; j++)
            {
                _cubes[i, j].transform.localPosition = currentPosition;
                currentPosition.x += _stepX;
            }

            currentPosition.x = startPosition.x;
            currentPosition.z += _stepZ;
        }
    }
}