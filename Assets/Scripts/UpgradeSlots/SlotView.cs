using TMPro;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class SlotView : MonoBehaviour
{
    private const float InActiveAlpha = 0.5f;
    private const float ActiveAlpha = 1.0f;

    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _priceText;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetVisible(int playerMoney, int upgradePrice)
    {
        _canvasGroup.alpha = playerMoney < upgradePrice ? InActiveAlpha : ActiveAlpha;
    }

    public void SetLevel(int level)
    {
        _levelText.text = level.ToString();
    }

    public void SetPrice(int price)
    {
        _priceText.text = price.ToString();
    }
}