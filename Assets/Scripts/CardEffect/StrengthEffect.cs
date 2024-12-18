using UnityEngine;
[CreateAssetMenu(fileName = "StrengthEffect", menuName = "Effects/StrengthEffect")]

public class StrengthEffect :Effect
{
    public override void Excute(CharacterBase from, CharacterBase Target)
    {
        bool isgood;
        int value0;
        if (value >= 0)
        {
            isgood = true;
            value0 = value;
        }
        else
        {
            isgood = false;
            value0 = -value;
        }
        
        switch (targetType)
        {
            case EffectTargetType.Self:
                from.SetUpStrength(value0, isgood);
                break;
            case EffectTargetType.Target:
                Target.SetUpStrength(value0, isgood);
                break;
            case EffectTargetType.All:
                foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    from.SetUpStrength(value0, isgood);
                    enemy.GetComponent<CharacterBase>().SetUpStrength(value0, isgood);
                }
                break;
        }
    }
}
