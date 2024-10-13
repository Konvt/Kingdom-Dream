using UnityEngine;

public abstract class Effect :ScriptableObject
{
    public int value;

    public EffectTargetType targetType;


    public abstract void Excute(CharacterBase from , CharacterBase Target);
}
