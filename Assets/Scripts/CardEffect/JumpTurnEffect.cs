using UnityEngine;
[CreateAssetMenu(fileName = "JumpTurnEffect", menuName = "Effects/JumpTurnEffect")]

public class JumpTurnEffect : Effect
{
    public override void Excute(CharacterBase from, CharacterBase Target)
    {
        
        switch (targetType)
        {
            case EffectTargetType.Self: //对自己的效果是下回合Mana置零
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