using UnityEngine;

[CreateAssetMenu(fileName = "DrawCardEffect", menuName = "Effects/DrawCardEffect")]

public class DrawCardEffect : Effect
{
    public IntEventSO drawCardEvnet;
    public override void Excute(CharacterBase from, CharacterBase Target)
    {
        drawCardEvnet?.RiseEvent(value,this);
    }
}
