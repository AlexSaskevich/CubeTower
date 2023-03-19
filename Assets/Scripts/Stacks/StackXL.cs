using UnityEngine;

public class StackXL : Stack
{
    [SerializeField] private CubeRemover _cubeRemover;

    private void OnEnable()
    {
        CapacitySlot.Upgraded += OnUpgraded;
        _cubeRemover.CubeRemoved += OnCubeRemoved;
        _cubeRemover.AllCubesRemoved += OnAllCubesRemoved;
    }

    private void OnDisable()
    {
        CapacitySlot.Upgraded -= OnUpgraded;
        _cubeRemover.CubeRemoved -= OnCubeRemoved;
        _cubeRemover.AllCubesRemoved -= OnAllCubesRemoved;
    }

    public override bool TryAdd(Cube cube)
    {
        if (base.TryAdd(cube) == false)
            return false;

        if (cube.Size == CubeSize.XL)
        {
            cube.GetComponent<Collider>().enabled = false;
            cube.GetComponent<CubeAnimation>().StartJump(this, cube);
        }

        return true;
    }

    private void OnCubeRemoved(Cube cube)
    {
        if (cube.Size == CubeSize.XL)
            ReducePosition(cube);
        else
            IncreaseCapacity();
    }

    private void OnAllCubesRemoved()
    {
        UpdateCapacity();
    }
}