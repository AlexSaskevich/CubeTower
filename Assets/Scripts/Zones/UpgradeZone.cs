using System.Collections.Generic;
using UnityEngine;

public class UpgradeZone : MonoBehaviour, IInteractable
{
    [SerializeField] private SlotsAnimation _slotsAnimation;
    [SerializeField] private List<GameObject> _slots;
    [SerializeField] private List<GameObject> _buttons;
    [SerializeField] private List<GameObject> _views;

    private void OnEnable()
    {
        _slotsAnimation.Finished += OnSlotsAnimationFinished;
    }

    private void OnDisable()
    {
        _slotsAnimation.Finished -= OnSlotsAnimationFinished;
    }

    public void Interact()
    {
        ShowSlots();
        HideButtons();
        HideViews();
    }

    public void StopInteract()
    {
        HideSlots();
    }

    private void ShowSlots()
    {
        _slotsAnimation.Show();

        foreach (var slot in _slots)
            slot.GetComponent<ISlot>().Show();
    }

    private void HideSlots()
    {
        _slotsAnimation.Hide();
    }

    private void ShowButtons()
    {
        foreach (var button in _buttons)
            button.GetComponent<IButton>().Show();
    }

    private void HideButtons()
    {
        foreach (var button in _buttons)
            button.GetComponent<IButton>().Hide();
    }

    private void HideViews()
    {
        foreach (var view in _views)
            view.GetComponent<IViewable>().Hide();
    }

    private void OnSlotsAnimationFinished()
    {
        ShowButtons();
    }
}