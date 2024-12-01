using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName ="DamageEffect",menuName ="Effects/DamageEffect")]
public class DamageEffect :Effect
{
    public override void Excute(CharacterBase from, CharacterBase Target)
    {
        if(Target==null) return;
        switch (targetType)
        {
            case EffectTargetType.Self:
                break;
            case EffectTargetType.Target:
                var damage = (int)math.round(from.baseStrength * value);
                Target.TakeDamage(damage);
                break;
            case EffectTargetType.All:
                //ÈºÌåÂß¼­
                foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    enemy.GetComponent<CharacterBase>().TakeDamage(value);
                }
                break;
        }
    }

  
}
