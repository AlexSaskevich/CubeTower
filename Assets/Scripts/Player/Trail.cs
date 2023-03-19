using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class Trail : MonoBehaviour
{
    [SerializeField] private Collector _collector;
    [SerializeField] private CubeRemover _cubeRemover;
    [SerializeField] private Stack _stack;
    [SerializeField] private List<Material> _materials;

    private TrailRenderer _trailRenderer;
    private Cube _lastCube;

    private void Awake()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
        _trailRenderer.emitting = false;
    }

    private void OnEnable()
    {
        _collector.CubeCollected += OnCubeCollected;
        _cubeRemover.CubeRemoved += OnCubeRemoved;
        _cubeRemover.AllCubesRemoved += OnAllCubesRemoved;
        _cubeRemover.Stopped += OnStopped;
    }

    private void OnDisable()
    {
        _collector.CubeCollected -= OnCubeCollected;
        _cubeRemover.CubeRemoved -= OnCubeRemoved;
        _cubeRemover.AllCubesRemoved -= OnAllCubesRemoved;
        _cubeRemover.Stopped -= OnStopped;
    }

    private void OnStopped(Cube cube)
    {
        if (cube == null)
            return;

        _lastCube = cube;
        _trailRenderer.startWidth = cube.Size;
        SetMaterial(cube.Size);
    }

    private void OnCubeCollected(Cube cube)
    {
        if (_stack.CurrentCapacity <= 1)
            return;

        if (_lastCube != null && cube.Size <= _lastCube.Size)
            return;

        _trailRenderer.emitting = true;
        _lastCube = cube;
        _trailRenderer.startWidth = _lastCube.Size;
        SetMaterial(_lastCube.Size);
    }

    private void OnCubeRemoved(Cube cube)
    {
        _lastCube = cube;
        _trailRenderer.startWidth = _lastCube.Size;
        SetMaterial(_lastCube.Size);
    }

    private void OnAllCubesRemoved()
    {
        _trailRenderer.emitting = false;
    }

    private void SetMaterial(float cubeSize)
    {
        _trailRenderer.material = cubeSize switch
        {
            CubeSize.XS => _materials[0],
            CubeSize.S => _materials[1],
            CubeSize.M => _materials[2],
            CubeSize.L => _materials[3],
            CubeSize.XL => _materials[4],
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}