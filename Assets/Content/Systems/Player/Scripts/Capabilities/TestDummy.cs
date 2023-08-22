using TMPro;
using UnityEngine;

public class TestDummy : MonoBehaviour, IDamagable
{
    [SerializeField] private TextMeshProUGUI damageTxt;
    private int _HitTracker = 0;
    private int _DamageTracker = 0;

    public void Damage(int _Damage)
    {
        _HitTracker++;
        _DamageTracker += _Damage;
        damageTxt.text = $"{_HitTracker} hits, dealing {_DamageTracker} \n Last hit dealt {_Damage}";
    }
}
