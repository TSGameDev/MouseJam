using TMPro;
using UnityEngine;

public class Currency : MonoBehaviour
{
    [SerializeField] private int soulCurrency = 0;
    [SerializeField] private TextMeshProUGUI soulCurrencyTxt;

    public bool CheckCurrency(int _AmountRequired) => soulCurrency >= _AmountRequired;
    public void AddCurrency(int _AmountToAdd)
    {
        soulCurrency += _AmountToAdd;
        soulCurrencyTxt.text = $"Soul Currency: {soulCurrency}";
    }
    public void RemoveCurrency(int _AmountToRemove)
    {
        soulCurrency -= _AmountToRemove;
        soulCurrencyTxt.text = $"Soul Currency: {soulCurrency}";
    }
}
