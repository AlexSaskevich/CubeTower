using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(ButtonAnimation))]
[RequireComponent(typeof(SlotView))]
[RequireComponent(typeof(Upgrader))]
[RequireComponent(typeof(Button))]
public class CapacitySlot : MonoBehaviour, ISlot
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private SpeedSlot _speedSlot;
    [SerializeField] private SizeSlot _sizeSlot;

    private ButtonAnimation _buttonAnimation;
    private SlotView _slotView;
    private Button _button;
    private Upgrader _upgrader;

    public event UnityAction<int> Upgraded;

    private void Awake()
    {
        _buttonAnimation = GetComponent<ButtonAnimation>();
        _slotView = GetComponent<SlotView>();
        _upgrader = GetComponent<Upgrader>();
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(TryUpgrade);
        _speedSlot.Upgraded += OnUpgraded;
        _sizeSlot.Upgraded += OnUpgraded;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(TryUpgrade);
        _speedSlot.Upgraded -= OnUpgraded;
        _sizeSlot.Upgraded -= OnUpgraded;
    }

    public void Show()
    {
        _slotView.SetVisible(_wallet.Money, _upgrader.CurrentPrice);
        _buttonAnimation.SetAnimation(_wallet.Money, _upgrader.CurrentPrice);
    }

    public void TryUpgrade()
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