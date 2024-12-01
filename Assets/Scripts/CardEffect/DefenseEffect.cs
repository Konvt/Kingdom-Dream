using UnityEngine;
[CreateAssetMenu(fileName = "DefenseEffect", menuName = "Effects/DefenseEffect")]

public class DefenseEffect : Effect
{
    public override void Excute(CharacterBase from, CharacterBase Target)
    {
        if (targetType == EffectTargetType.Self)
        {
            from.UpdateDefense(value);
        }
        if (targetType == EffectTargetType.Target)
        {
            Target.UpdateDefense(value);
        }
    }
}
