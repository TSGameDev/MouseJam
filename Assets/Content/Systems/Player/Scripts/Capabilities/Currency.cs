using UnityEngine;

public class Currency : MonoBehaviour
{
    private int soulCurrency = 0;

    public bool CheckCurrency(int _AmountRequired) => soulCurrency >= _AmountRequired;
    public void AddCurrency(int _AmountToAdd) => soulCurrency += _AmountToAdd;
    public void RemoveCurrency(int _AmountToRemove) => soulCurrency -= _AmountToRemove;
}
