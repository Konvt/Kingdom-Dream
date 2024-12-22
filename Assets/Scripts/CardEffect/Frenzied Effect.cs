using UnityEngine;

/// <summary>
/// 如果当前生命值>50% 则提升50%攻击力，持续value个回合
/// </summary>
[CreateAssetMenu(fileName = "Frenzied Effect", menuName = "Effects/Frenzied")]
public class FrenziedEffect : Effect
{
    public override void Excute(CharacterBase from, CharacterBase Target)
    {
        if(from.hp.currentValue * 2 < from.hp.maxValue)
        {
            from.SetUpStrength(value, true);
        }
    }
}