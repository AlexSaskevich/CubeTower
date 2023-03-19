using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CubeAnimation : MonoBehaviour
{
    [SerializeField] private float _jumpDuration;
    [SerializeField] private float _throwDuration;
    [SerializeField] private Transform _targetThrowPoint;
    [SerializeField] private PlayerAnimation _playerAnimation;

    private Transform _start;
    private Transform _p1;
    private Transform _p2;
    private Coroutine _jumpCoroutine;
    private Coroutine _throwCoroutine;
    private float _halfThrowDuration;

    public event UnityAction Throwed;

    private void Start()
    {
        _start = transform.parent;
        _p1 = transform.parent.GetChild(1);
        _p2 = transform.parent.GetChild(2);
        _halfThrowDuration = _throwDuration / 2;
    }

    public void StartJump(Stack stack, Cube cube)
    {
        if (_jumpCoroutine != null)
            StopCoroutine(Jump(stack, cube));

        _jumpCoroutine = StartCoroutine(Jump(stack, cube));
    }

    public void StartThrow(Cube cube)
    {
        if (_throwCoroutine != null)
            StopCoroutine(Throw(cube));

        _throwCoroutine = StartCoroutine(Throw(cube));
    }

    private IEnumerator Jump(Stack stack, Cube cube)
    {
        Vector3 targetposition = new(0, cube.Size * stack.transform.childCount, 0);
        Vector3 startPosition = _start.position;

        float time = 0;

        _playerAnimation.Jump(cube.Size);

        while (time < _jumpDuration)
        {
            cube.transform.position = Bezier.GetPoint(startPosition, _p1.position, _p2.position, stack.transform.position + targetposition, time / _jumpDuration);
            time += Time.deltaTime;
            yield return null;
        }

        cube.transform.SetParent(stack.transform, true);
        cube.transform.localPosition = new Vector3(0, cube.Size * (stack.transform.childCount - 1), 0);
        cube.transform.localRotation = Quaternion.identity;
    }

    private IEnumerator Throw(Cube cube)
    {
        Vector3 startPosition = cube.transform.position;
        cube.transform.parent = null;
        cube.transform.rotation = Quaternion.identity;

        float time = 0;

        _playerAnimation.Fall(cube.Size);

        while (time < _throwDuration)
        {
            cube.transform.position = Vector3.Lerp(startPosition, _targetThrowPoint.position, time / _throwDuration);
            time += Time.deltaTime;

            if (Math.Round(time, 2) == _halfThrowDuration)
                Throwed?.Invoke();

            yield return null;
        }

        cube.transform.position = _targetThrowPoint.position;

        Destroy(cube.gameObject);
    }
}