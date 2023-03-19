using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CubeRemover : MonoBehaviour
{
    [SerializeField] private float _cubeRemoveTime;

    private readonly List<Stack> _stacks = new();
    private readonly List<Cube> _cubes = new();
    private Coroutine _deleteCoroutine;
    private WaitForSeconds _waitForSeconds;

    public event UnityAction<Cube> CubeRemoved;
    public event UnityAction AllCubesRemoved;
    public event UnityAction<Cube> Stopped;

    private void Start()
    {
        _waitForSeconds = new WaitForSeconds(_cubeRemoveTime);
        GetStacks();
    }

    public void StartDeleteCubes()
    {
        if (_deleteCoroutine != null)
            StopCoroutine(DeleteCubes());

        _deleteCoroutine = StartCoroutine(DeleteCubes());
    }

    public void StopDeleteCubes()
    {
        if (_deleteCoroutine == null)
            return;

        StopCoroutine(_deleteCoroutine);

        _cubes.Clear();

        GetCubes();

        for (int i = _stacks.Count - 1; i >= 0; i--)
        {
            if (_stacks[i].transform.childCount == 0)
                _stacks[i].transform.localPosition = Vector3.zero;

            if (_stacks[i].transform.localPosition.y >= 0)
                continue;

            float stackPositionY = Mathf.Abs(_stacks[i].transform.localPosition.y);
            _stacks[i].transform.localPosition = Vector3.zero;

            for (int j = 0; j < _stacks[i].transform.childCount; j++)
            {
                Vector3 newCubePosition = new(0, _cubes[j].transform.localPosition.y - stackPositionY, 0);
                _stacks[i].transform.GetChild(j).localPosition = newCubePosition;
            }
        }

        if (_cubes.Count == 0)
            AllCubesRemoved?.Invoke();

        Stopped?.Invoke(_cubes.FirstOrDefault());
    }

    private IEnumerator DeleteCubes()
    {
        _cubes.Clear();
        GetCubes();

        for (int i = 0; i < _cubes.Count; i++)
        {
            _cubes[i].GetComponent<CubeAnimation>().StartThrow(_cubes[i]);
            CubeRemoved?.Invoke(_cubes[i]);
            yield return _waitForSeconds;
        }

        _cubes.Clear();

        SetStackPositionToZero();

        AllCubesRemoved?.Invoke();
    }

    private void GetStacks()
    {
        for (int i = 0; i < transform.childCount; i++)
            _stacks.Add(transform.GetChild(i).GetComponent<Stack>());
    }

    private void GetCubes()
    {
        for (int i = _stacks.Count - 1; i >= 0; i--)
            for (int j = 0; j < _stacks[i].transform.childCount; j++)
                _cubes.Add(_stacks[i].transform.GetChild(j).GetComponent<Cube>());
    }

    private void SetStackPositionToZero()
    {
        for (int i = 0; i < _stacks.Count; i++)
            _stacks[i].transform.localPosition = Vector3.zero;
    }
}