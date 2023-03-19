using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private TMP_Text _currencyText;

    private void OnEnable()
    {
        _wallet.MoneyAmountChanged += OnMoneyChanged;
    }

    private void OnDisable()
    {
        _wallet.MoneyAmountChanged -= OnMoneyChanged;
    }

    private void OnMoneyChanged(int value)
    {
        _currencyText.text = AbbreviationUutility.ConvertMoney(value);
    }
}