using UnityEngine;
[CreateAssetMenu(fileName = "DefenseDamageEffect", menuName = "Effects/DefenseDamageEffect")]

public class DefenseDamageEffect : Effect
{
    public override void Excute(CharacterBase from, CharacterBase Target)
    {
        Target.TakeDamage(from.defense.currentValue);
    }

  
}
