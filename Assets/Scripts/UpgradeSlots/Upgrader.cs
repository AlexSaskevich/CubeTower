using UnityEngine;

[RequireComponent(typeof(SlotView))]
[RequireComponent(typeof(ButtonAnimation))]
public class Upgrader : MonoBehaviour
{
    private const int DefaultLevel = 1;

    [SerializeField] private Wallet _wallet;
    [SerializeField] private int _defaultPrice;
    [SerializeField] private int _priceModifier;

    private SlotView _slotView;
    private ButtonAnimation _buttonAnimation;

    public int CurrentPrice { get; private set; }
    public int CurrentLevel { get; private set; }

    private void Awake()
    {
        CurrentLevel = DefaultLevel;
        CurrentPrice = _defaultPrice;
        _slotView = GetComponent<SlotView>();
        _buttonAnimation = GetComponent<ButtonAnimation>();
    }

    public bool TryUpgrade()
    {
        if (_wallet.Money < CurrentPrice)
            return false;

        _wallet.RemoveCurrency(CurrentPrice);
        CurrentLevel++;
        CurrentPrice += _priceModifier;
        _slotView.SetVisible(_wallet.Money, CurrentPrice);
        _slotView.SetLevel(CurrentLevel);
        _slotView.SetPrice(CurrentPrice);
        _buttonAnimation.SetAnimation(_wallet.Money, CurrentPrice);
        return true;
    }
}