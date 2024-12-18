using UnityEngine;
[CreateAssetMenu(fileName = "DefenseDoubleEffect", menuName = "Effects/DefenseDoubleEffect")]

public class DefenseDoubleEffect : Effect
{
    public override void Excute(CharacterBase from, CharacterBase Target)
    {
        if (targetType == EffectTargetType.Self)
        {
            var Defence_temp0 = from.defense.currentValue;
            from.UpdateDefense(Defence_temp0);
        }
        if (targetType == EffectTargetType.Target)
        {
            var Defence_temp = Target.defense.currentValue;
            Target.UpdateDefense(value);
        }
    }
}
