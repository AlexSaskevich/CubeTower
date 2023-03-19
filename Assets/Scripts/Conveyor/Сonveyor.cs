using System.Collections;
using UnityEngine;

public class Сonveyor : MonoBehaviour
{
    private const float Delta = 0.08f;

    [SerializeField] private CubeRemover _cubeRemover;
    [SerializeField] private int _dollarPerCube;
    [SerializeField] private Transform _startInstantiate;
    [SerializeField] private int _force;
    [SerializeField] private float _timeBetweenSpawn;
    [SerializeField] private GameObject _dollar;

    private WaitForSeconds _waitForSeconds;
    private float _positionX;
    private float _positionY;
    private float _positionZ;

    private void OnEnable()
    {
        _cubeRemover.CubeRemoved += OnCubeRemoved;
    }

    private void OnDisable()
    {
        _cubeRemover.CubeRemoved -= OnCubeRemoved;
    }

    private void Start()
    {
        _waitForSeconds = new WaitForSeconds(_timeBetweenSpawn);
        _positionX = _startInstantiate.position.x;
        _positionY = _startInstantiate.position.y;
        _positionZ = _startInstantiate.position.z;
    }

    private void OnCubeRemoved(Cube cube)
    {
        StartCoroutine(Instantiate());
    }

    private IEnumerator Instantiate()
    {
        for (int i = 0; i < _dollarPerCube; i++)
        {
            GameObject dollar = Instantiate(_dollar, GetRandomVector3(), Quaternion.identity);
            dollar.GetComponent<Rigidbody>().AddForce(Vector3.back * _force);
            yield return _waitForSeconds;
        }
    }

    private Vector3 GetRandomVector3()
    {
        return new Vector3(Random.Range(_positionX - Delta, _positionX + Delta), _positionY, _positionZ);
    }
}