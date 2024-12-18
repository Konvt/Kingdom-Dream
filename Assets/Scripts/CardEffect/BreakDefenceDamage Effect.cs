using System;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "BreakDefenceDamageEffect", menuName = "Effects/BreakDefenceDamageEffect")]
public class BreakDefenceDamageEffect : Effect
{
    public override void Excute(CharacterBase from, CharacterBase Target)
    {
        if (Target == null) return;
        int value0;
        switch (targetType)
        {
            case EffectTargetType.Self:              
                from.defense.SetValue(0);
                break;
            case EffectTargetType.Target:
                value0 = Target.defense.currentValue;
                Target.TakeDamage(value0);
                break;
            case EffectTargetType.All:
                //ÈºÌåÂß¼­
                foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    value0 = enemy.GetComponent<CharacterBase>().defense.currentValue;
                    enemy.GetComponent<CharacterBase>().TakeDamage(value0);
                }
                break;
        }
    }


}
