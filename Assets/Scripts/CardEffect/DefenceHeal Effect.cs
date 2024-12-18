using System;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "DefenceHealEffect", menuName = "Effects/DefenceHealEffect")]

public class DefenceHealEffect : Effect
{
    public override void Excute(CharacterBase from, CharacterBase target)
    {
        if (targetType == EffectTargetType.Self)    //value的值填1-10，代表恢复护盾值的一到十成；自己用把护盾消耗掉
        {
            var value0 = (int)math.round(from.defense.currentValue * value / 10);
            from.defense.SetValue(0);
            from.Heal(value0);
        }

        else if (targetType == EffectTargetType.Target) //敌人应该用不到
        {
            //var value0 = target.defense.currentValue * value / 10;
            //target.Heal(value0);
        }
        else if (targetType == EffectTargetType.All)    //敌人应该用不到
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
