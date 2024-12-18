using UnityEngine;

[CreateAssetMenu(fileName = "CanNotBeAttackEffect", menuName = "Effects/CanNotBeAttackEffect")]

public class CanNotBeAttackEffect : Effect
{
    
    public override void Excute(CharacterBase from, CharacterBase Target) 
    {
        switch (targetType)
        {
            case EffectTargetType.Self:
                from.ChangeCanNotBeAttack(value);
                break;
            case EffectTargetType.Target:
                Target.ChangeCanNotBeAttack(value);
                break;
            case EffectTargetType.All:       //�����޷����κ�һ����������˺�
                foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    
                    enemy.GetComponent<CharacterBase>().ChangeCanNotBeAttack(value);
                }
                break;
        }
        
    }
}
