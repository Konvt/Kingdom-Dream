using UnityEngine;

/// <summary>
/// 有(value / 10)概率使得Target的攻击增益置零
/// </summary>
[CreateAssetMenu(fileName = "RinseEffect Effect", menuName = "Effects/Rinse")]
public class RinseEffect : Effect
{
    public override void Excute(CharacterBase from, CharacterBase Target)
    {
        if (Target.baseStrength <= 1) return;
        var roll = Random.Range(0, 100);
        if (roll <= value * 10)
        {
            Target.SetUpStrength(Target.strengthRound.currentValue, false);
        }
    }
}