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
                Target.TakeDamage(value);
                Debug.Log("造成了："+value+" 点伤害");
                break;
            case EffectTargetType.All:
                //群体逻辑
                foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    enemy.GetComponent<CharacterBase>().TakeDamage(value);
                }
                break;
        }
    }

  
}
