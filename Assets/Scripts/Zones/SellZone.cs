using UnityEngine;

public class SellZone : MonoBehaviour, IInteractable
{
    [SerializeField] private CubeRemover _cubeRemover;

    public void Interact()
    {
        _cubeRemover.StartDeleteCubes();
    }

    public void StopInteract()
    {
        _cubeRemover.StopDeleteCubes();
    }
}