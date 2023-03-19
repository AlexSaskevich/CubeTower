using System.Collections;
using TMPro;
using UnityEngine;

public class TypingTextAnimation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _desctiptionText;
    [SerializeField] private float _typingDelay = 0.075f;

    private string _text;
    private WaitForSeconds _waitForSeconds;

    private void Start()
    {
        _text = _desctiptionText.text;
        _desctiptionText.text = string.Empty;
        _waitForSeconds = new WaitForSeconds(_typingDelay);
    }

    public IEnumerator Type()
    {
        _desctiptionText.text = string.Empty;

        foreach (char symbol in _text)
        {
            _desctiptionText.text += symbol;
            yield return _waitForSeconds;
        }
    }
}