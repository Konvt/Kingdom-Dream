using UnityEngine;

/// <summary>
/// 根据自身当前生命值与上限，获得一定护盾
/// 最低获得1点护盾，最高获得value点护盾,
/// 提升数值向下取整
/// </summary>
[CreateAssetMenu(fileName = "Incandesce Effect", menuName = "Effects/Incandesce")]
public class IncandesceEffect : Effect
{
    public override void Excute(CharacterBase from, CharacterBase Target)
    {
        var hpObj = from.hp;
        int fix = (int)Mathf.Lerp(value, 1, (float)hpObj.currentValue / hpObj.maxValue);
        from.UpdateDefense(fix);
    }
}