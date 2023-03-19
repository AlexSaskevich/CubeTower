using Lean.Localization;
using System.Collections.Generic;
using UnityEngine;

public class Text3D : MonoBehaviour
{
    [SerializeField] private Language _language;
    [SerializeField] private List<GameObject> _textGameObjects = new();

    private void OnEnable()
    {
        _language.LanguageChanged += OnLanguageChanged;
    }

    private void OnDisable()
    {
        _language.LanguageChanged -= OnLanguageChanged;
    }

    private void Start()
    {
        string currentLanguage = LeanLocalization.GetFirstCurrentLanguage();

        foreach (var item in _textGameObjects)
            item.SetActive(item.name == currentLanguage);
    }

    private void OnLanguageChanged(string language)
    {
        foreach (var item in _textGameObjects)
            item.SetActive(item.name == language);
    }
}