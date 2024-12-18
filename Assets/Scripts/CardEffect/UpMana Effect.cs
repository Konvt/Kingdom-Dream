using System;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "UpManaEffect", menuName = "Effects/UpManaEffect")]

public class UpManaEffect : Effect
{ 
    public override void Excute(CharacterBase from, CharacterBase Target)
    {
        foreach (var player0 in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (value >= 0)
            {
                player0.GetComponent<Player>().UpMana(value);
            }
            else
            {
                if (from.CannotBeAttack != 0)   //用来满足那张无法被攻击时可获得一个MaNa的牌的效果
                {
                    player0.GetComponent<Player>().UpMana(-value);
                }
            }
            
        }
    }
}
