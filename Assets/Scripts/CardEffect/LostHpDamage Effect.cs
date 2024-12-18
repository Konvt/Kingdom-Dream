using System;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "LostHpDamageEffect", menuName = "Effects/LostHpDamageEffect")]
public class LostHpDamageEffect : Effect
{
    public override void Excute(CharacterBase from, CharacterBase Target)      //value����ֵΪ0-100����ʾ�ٷ�֮����
    {
        if (Target == null) return;
        switch (targetType)
        {
            case EffectTargetType.Self:
                var damage00 = (int)math.round((from.maxHp - from.hp.currentValue) * value / 100);
                from.TakeDamage(damage00);
                break;
            case EffectTargetType.Target:
                var damage = (int)math.round((Target.maxHp - Target.hp.currentValue) * value / 100);
                Target.TakeDamage(damage);
                break;
            case EffectTargetType.All:
                //Ⱥ���߼�
                foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    var damage0 = (int)math.round((enemy.GetComponent<CharacterBase>().maxHp - enemy.GetComponent<CharacterBase>().hp.currentValue) * value / 100);
                    enemy.GetComponent<CharacterBase>().TakeDamage(damage0);
                }
                break;
        }
    }


}
