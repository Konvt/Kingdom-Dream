using UnityEngine;

[CreateAssetMenu(fileName = "HealEffect", menuName = "Effects/HealEffect")]

public class HealEffect : Effect
{
    public override void Excute(CharacterBase from, CharacterBase target)
    {
        if(targetType==EffectTargetType.Self)
        from.Heal(value);
        else if(targetType==EffectTargetType.Target)
        target.Heal(value);
    }
}
