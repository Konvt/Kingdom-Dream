using UnityEngine;

/// <summary>
/// ��������ǰ����ֵ�����ޣ����һ������
/// ��ͻ��1�㻤�ܣ���߻��value�㻤��,
/// ������ֵ����ȡ��
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