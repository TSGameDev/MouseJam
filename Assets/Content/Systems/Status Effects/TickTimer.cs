using UnityEngine;

//Class to spawn on enemy to track individual status effects
public class TickTimer : MonoBehaviour
{
    private int _CurrentTickCount = 0;
    private int _MaxTickCount;
    public void SetMaxTickCount(int _MaxTicks) => _MaxTickCount = _MaxTicks;
    private IStatusEffect _StatusEffect;
    public void SetStatusEffect(IStatusEffect _Effect) => _StatusEffect = _Effect;
    private IEffectable _Target;
    public void SetTarget(IEffectable _Target) => this._Target = _Target;

    private bool _IsPassive;
    public void SetIsPassive(bool _IsPassive) => this._IsPassive = _IsPassive;

    public void ActivateStatusEffect(object sender, TimeTickSystem.OnTickEventArgs e)
    {
        if (_CurrentTickCount >= _MaxTickCount && _IsPassive == false)
        {
            switch (_StatusEffect.GetTickTime())
            {
                case TickTime.EveryTick:
                    TimeTickSystem.OnTick -= ActivateStatusEffect;
                    break;
                case TickTime.Every5Tick:
                    TimeTickSystem.OnTick_5 -= ActivateStatusEffect;
                    break;
            }

            _StatusEffect.RemoveStatusEffect(_Target);
            Destroy(gameObject);
        }
        else if(_IsPassive == false)
        {
            _CurrentTickCount++;
            _StatusEffect.ApplyStatusEffect(_Target);
        }
        else
        {
            _StatusEffect.ApplyStatusEffect(_Target);
        }
    }

    public static void CreateTickTimer(GameObject _Parent, IEffectable _TargetEffectable, IStatusEffect _Effect)
    {
        //Create new timer object, add ticktimer class and make its parent this game object
        GameObject _NewTickTimerObj = new GameObject(_Effect.GetEffectName());
        _NewTickTimerObj.transform.parent = _Parent.transform;
        TickTimer _NewTickTimer = _NewTickTimerObj.AddComponent<TickTimer>();
        _NewTickTimer.SetStatusEffect(_Effect);
        _NewTickTimer.SetMaxTickCount(_Effect.GetMaxTick());
        _NewTickTimer.SetTarget(_TargetEffectable);
        _NewTickTimer.SetIsPassive(_Effect.GetIsPassive());
        switch (_Effect.GetTickTime())
        {
            case TickTime.EveryTick:
                TimeTickSystem.OnTick += _NewTickTimer.ActivateStatusEffect;
                break;
            case TickTime.Every5Tick:
                TimeTickSystem.OnTick_5 += _NewTickTimer.ActivateStatusEffect;
                break;
        }
    }
}
