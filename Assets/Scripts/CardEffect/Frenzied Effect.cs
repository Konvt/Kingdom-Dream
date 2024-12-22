using UnityEngine;

/// <summary>
/// �����ǰ����ֵ>50% ������50%������������value���غ�
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