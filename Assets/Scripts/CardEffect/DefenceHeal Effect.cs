using System;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "DefenceHealEffect", menuName = "Effects/DefenceHealEffect")]

public class DefenceHealEffect : Effect
{
    public override void Excute(CharacterBase from, CharacterBase target)
    {
        if (targetType == EffectTargetType.Self)    //value��ֵ��1-10������ָ�����ֵ��һ��ʮ�ɣ��Լ��ðѻ������ĵ�
        {
            var value0 = (int)math.round(from.defense.currentValue * value / 10);
            from.defense.SetValue(0);
            from.Heal(value0);
        }

        else if (targetType == EffectTargetType.Target) //����Ӧ���ò���
        {
            //var value0 = target.defense.currentValue * value / 10;
            //target.Heal(value0);
        }
        else if (targetType == EffectTargetType.All)    //����Ӧ���ò���
        {
            //foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            //{
              //  var value0 = enemy.GetComponent<CharacterBase>().defense.currentValue * value / 10;
               // enemy.GetComponent<CharacterBase>().Heal(value0);
          //  }
        }
        if (soumdVFX != null)
            AudioPlayer.instance.PlayVFX(soumdVFX);
    }
}
