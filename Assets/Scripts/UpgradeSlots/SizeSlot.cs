using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(ButtonAnimation))]
[RequireComponent(typeof(SlotView))]
[RequireComponent(typeof(Upgrader))]
[RequireComponent(typeof(Button))]
public class SizeSlot : MonoBehaviour, ISlot
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private SpeedSlot _speedSlot;
    [SerializeField] private CapacitySlot _capacitySlot;

    private ButtonAnimation _buttonAnimation;
    private SlotView _slotView;
    private Button _button;
    private Upgrader _upgrader;

    public event UnityAction<int> Upgraded;

    public int MaxCubeSizeLevel { get; private set; }

    private void Awake()
    {
        _buttonAnimation = GetComponent<ButtonAnimation>();
        _slotView = GetComponent<SlotView>();
        _button = GetComponent<Button>();
        _upgrader = GetComponent<Upgrader>();
        MaxCubeSizeLevel = Enum.GetNames(typeof(AvailableCubeSizeLevel)).Length;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(TryUpgrade);
        _speedSlot.Upgraded += OnUpgraded;
        _capacitySlot.Upgraded += OnUpgraded;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(TryUpgrade);
        _speedSlot.Upgraded -= OnUpgraded;
        _capacitySlot.Upgraded -= OnUpgraded;
    }

    public void Show()
    {
        _slotView.SetVisible(_wallet.Money, _upgrader.CurrentPrice);
        _buttonAnimation.SetAnimation(_wallet.Money, _upgrader.CurrentPrice);
    }

    private void TryUpgrade()
    {
        if (_upgrader.TryUpgrade() == false)
            return;

        Upgraded?.Invoke(_upgrader.CurrentLevel);
    }

    private void OnUpgraded(int level)
    {
        Show();
    }
}