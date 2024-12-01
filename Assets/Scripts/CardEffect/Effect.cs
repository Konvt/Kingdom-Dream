using UnityEngine;

public abstract class Effect :ScriptableObject
{
    [Header("Ч������ֵ��С")]
    public int value;

    [Header("Ч�����ͷ�����")]

    public EffectTargetType targetType;


    public abstract void Excute(CharacterBase from , CharacterBase Target);
}
