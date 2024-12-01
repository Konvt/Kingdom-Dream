using UnityEngine;
[CreateAssetMenu(fileName = "StrengthEffect", menuName = "Effects/StrengthEffect")]

public class StrengthEffect :Effect
{
    public override void Excute(CharacterBase from, CharacterBase Target)
    {
        switch (targetType)
        {
            case EffectTargetType.Self:
                from.SetUpStrength(value, true);
                break;
            case EffectTargetType.Target:
                Target.SetUpStrength(value, false);
                break;
            case EffectTargetType.All:
                break;
        }
    }
}
