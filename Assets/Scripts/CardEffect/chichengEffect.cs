using System;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "chichengDamageEffect", menuName = "Effects/chichengDamageEffect")]
public class chichengDamageEffect : Effect
{
    public override void Excute(CharacterBase from, CharacterBase Target)
    {
        if (Target == null) return;

        if(targetType == EffectTargetType.Target)   //此效果只用于攻击单一敌人
        {
            int value0 = value;
            if(from.hp.currentValue == from.maxHp)
            {
                value0 += 1;
                from.UpdateDefense(1);
            }
            var damage = (int)math.round(from.baseStrength * value0);
            Target.TakeDamage(damage);
        }

    }


}
