using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private ProgressBar _progressBar;

    private bool _canInterract = true;

    public event UnityAction StartedSelling;

    private void OnEnable()
    {
        _progressBar.Reached += OnReached;
    }

    private void OnDisable()
    {
        _progressBar.Reached -= OnReached;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_canInterract == false)
            return;

        if (other.TryGetComponent(out IInteractable interactable))
            interactable.Interact();

        if (interactable is SellZone)
            StartedSelling?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
            interactable.StopInteract();
    }

    private void OnReached()
    {
        _canInterract = false;
    }
}