using Agava.YandexGames;
using Lean.Localization;
using UnityEngine;

public class Localization : MonoBehaviour
{
    private const string EnglishCode = "en";
    private const string RussianCode = "ru";
    private const string TurkishCode = "tr";
    private const string English = "English";
    private const string Russian = "Russian";
    private const string Turkish = "Turkish";

    private void Awake()
    {
        ChangeLanguage();
    }

    private void ChangeLanguage()
    {
        string languageCode = YandexGamesSdk.Environment.i18n.lang;

        string language = languageCode switch
        {
            EnglishCode => English,
            RussianCode => Russian,
            TurkishCode => Turkish,
            _ => English
        };

        LeanLocalization.SetCurrentLanguageAll(language);
    }
}