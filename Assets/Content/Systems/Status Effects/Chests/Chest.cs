using UnityEngine;

public class Chest : MonoBehaviour, IInteract
{
    [SerializeField] private StatusEffectSOBase[] randomChestEffects;
    [SerializeField] private int soulPrice;

    public void Interact(GameObject _Player, Currency _PlayerCurreny)
    {
        if (!_PlayerCurreny.CheckCurrency(soulPrice))
            return;

        //Reduce soul amount based on chest price
        _PlayerCurreny.RemoveCurrency(soulPrice);

        int _RandomNumber = Random.Range(0, randomChestEffects.Length);
        StatusEffectSOBase _randomEffect = randomChestEffects[_RandomNumber];
        IEffectable _TargetEffector = _Player.GetComponent<IEffectable>();
        if (_randomEffect.GetIsInstant())
        {
            _randomEffect.ApplyStatusEffect(_TargetEffector);
        }
        else if(_randomEffect.GetIsPassive())
        {
            _TargetEffector.GetActivePassives().Add(_randomEffect);
            _randomEffect.ApplyStatusEffect(_TargetEffector);
        }
        else
        {
            TickTimer.CreateTickTimer(_Player, _TargetEffector, _randomEffect);
        }
    }
}
