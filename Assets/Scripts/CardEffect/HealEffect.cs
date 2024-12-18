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

        else if (targetType == EffectTargetType.Target) //�������˻�Ѫ�����˿�����
        {
            target.Heal(value);
        }
        else if (targetType == EffectTargetType.All)    //ȫ��з���λ��Ѫ
        {
            foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    enemy.GetComponent<CharacterBase>().Heal(value);
                }
        }
        if(soumdVFX!=null)
        AudioPlayer.instance.PlayVFX(soumdVFX);
    }
}
