using UnityEngine;

public static class AbbreviationUutility
{
    public static string ConvertMoney(int money)
    {
        float value = money;
        string result;
        string[] scoreNames = { "", "k", "M" };
        int i;

        for (i = 0; i < scoreNames.Length - 1; i++)
            if (value < 1000)
                break;
            else
                value = Mathf.Floor(value / 100f) / 10f;

        if (value == Mathf.Floor(value))
            result = value.ToString() + scoreNames[i];
        else
            result = value.ToString("F1") + scoreNames[i];

        return result;
    }
}