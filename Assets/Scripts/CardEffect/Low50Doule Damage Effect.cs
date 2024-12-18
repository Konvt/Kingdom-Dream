using System;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "Low50DoubleDamageEffect", menuName = "Effects/Low50DoubleDamageEffect")]
public class Low50DoubleDamageEffect : Effect
{
    public override void Excute(CharacterBase from, CharacterBase Target)
    {
        if (Target == null) return;
        switch (targetType)
        {
            case EffectTargetType.Self:
                var damage00 = (int)math.round(from.baseStrength * value);
                if (from.hp.currentValue < from.maxHp)
                {
                    from.TakeDamage(damage00 * 2);
                }
                else
                {
                    from.TakeDamage(damage00);
                }      
                break;
            case EffectTargetType.Target:        //这个攻击效果一般只用于攻击敌人
                var damage = (int)math.round(from.baseStrength * value);
                if (from.hp.currentValue < from.maxHp)
                {
                    Target.TakeDamage(damage * 2);
                }
                else
                {
                    Target.TakeDamage(damage);
                }                
                break;
            case EffectTargetType.All:
                //群体逻辑
                var damage0 = (int)math.round(from.baseStrength * value);

                foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    if (from.hp.currentValue < from.maxHp)
                    {
                        enemy.GetComponent<CharacterBase>().TakeDamage(damage0*2);
                    }
                    else
                    {
                        enemy.GetComponent<CharacterBase>().TakeDamage(damage0);
                    } 
                }
                break;
        }
    }


}

