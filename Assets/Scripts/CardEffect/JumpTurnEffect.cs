using UnityEngine;
[CreateAssetMenu(fileName = "JumpTurnEffect", menuName = "Effects/JumpTurnEffect")]

public class JumpTurnEffect : Effect
{
    public override void Excute(CharacterBase from, CharacterBase Target)
    {
        
        switch (targetType)
        {
            case EffectTargetType.Self: //���Լ���Ч�����»غ�Mana����
                from.ChangeJumpTurn();
                break;
            case EffectTargetType.Target:
                Target.ChangeJumpTurn();
                break;
            case EffectTargetType.All:
                break;
        }
        
    }
}