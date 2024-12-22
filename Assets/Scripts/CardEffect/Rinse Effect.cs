using UnityEngine;

/// <summary>
/// ��(value / 10)����ʹ��Target�Ĺ�����������
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