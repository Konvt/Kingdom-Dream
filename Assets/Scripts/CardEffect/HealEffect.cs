using UnityEngine;

[CreateAssetMenu(fileName = "HealEffect", menuName = "Effects/HealEffect")]

public class HealEffect : Effect
{
    public override void Excute(CharacterBase from, CharacterBase target)
    {
        if (targetType == EffectTargetType.Self)
        {
            from.Heal(value);
        }

        else if (targetType == EffectTargetType.Target) //给其他人回血，敌人可能用
        {
            target.Heal(value);
        }
        if(soumdVFX!=null)
        AudioPlayer.instance.PlayVFX(soumdVFX);
    }
}
