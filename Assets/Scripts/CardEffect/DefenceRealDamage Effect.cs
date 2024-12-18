using System;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "DefencerealDamageEffect", menuName = "Effects/DefencerealDamageEffect")]
public class DefencerealDamageEffect : Effect
{
    public override void Excute(CharacterBase from, CharacterBase Target)
    {
        if (Target == null) return;
        var realdamage = Target.defense.currentValue;
        switch (targetType)
        {
            case EffectTargetType.Self:
                var damage00 = (int)math.round(from.baseStrength * realdamage);
                from.ThroughDefenceDamage(damage00);
                break;
            case EffectTargetType.Target:
                var damage = (int)math.round(from.baseStrength * realdamage);
                Target.ThroughDefenceDamage(damage);
                break;
            case EffectTargetType.All:
                //Ⱥ���߼�
                foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    var damage0 = (int)math.round(from.baseStrength * realdamage);
                    enemy.GetComponent<CharacterBase>().ThroughDefenceDamage(damage0);
                }
                break;
        }
    }

}
