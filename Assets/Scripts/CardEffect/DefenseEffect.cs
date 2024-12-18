using UnityEngine;
[CreateAssetMenu(fileName = "DefenseEffect", menuName = "Effects/DefenseEffect")]

public class DefenseEffect : Effect
{
    public override void Excute(CharacterBase from, CharacterBase Target)
    {
        if (targetType == EffectTargetType.Self) 
        {
            from.UpdateDefense(value);
        }
        if (targetType == EffectTargetType.Target) //给对方加的护盾会延续到敌方的回合结束
        {
            Target.NoResetDefence(value);
            Target.UpdateDefense(value);
        }
    }
}
