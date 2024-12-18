using System;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "yimaogongdunEffect", menuName = "Effects/yimaogongdunEffect")]
public class yimaogongdunEffect : Effect
{
    public override void Excute(CharacterBase from, CharacterBase Target)
    {
        if (Target == null) return;

        if (targetType == EffectTargetType.Target)   //此效果只用于攻击单一敌人
        {
            int value0 = value;
            if (Target.defense.currentValue != 0)
            {
                value0 += 4;

            }
            var damage = (int)math.round(from.baseStrength * value0);
            Target.TakeDamage(damage);
        }

    }


}
