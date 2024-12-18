using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "ThroughDefenceDamageEffect", menuName = "Effects/ThroughDefenceDamageEffect")]
public class ThroughDefenceDamageEffect : Effect
{
    public override void Excute(CharacterBase from, CharacterBase Target)
    {
        if (Target == null) return;
        switch (targetType)
        {
            case EffectTargetType.Self:
                var damage00 = (int)math.round(from.baseStrength * value);
                from.ThroughDefenceDamage(damage00);
                break;
            case EffectTargetType.Target:
                var damage = (int)math.round(from.baseStrength * value);
                Target.ThroughDefenceDamage(damage);
                break;
            case EffectTargetType.All:
                //ÈºÌåÂß¼­
                foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    var damage0 = (int)math.round(from.baseStrength * value);
                    enemy.GetComponent<CharacterBase>().ThroughDefenceDamage(damage0);
                }
                break;
        }
    }

}
