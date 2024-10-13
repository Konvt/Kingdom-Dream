using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName ="CardDataSO",menuName = "Card/CardDataSO")]
public class CardDataSO : ScriptableObject
{
    public string cardName;
    public Sprite cardImage;
    public int cost;
    public CardType cardType;
    [TextArea]
    public string cardDescription;

    //ʵ�ʵ�ִ��Ч��
    public List<Effect> effects;
}
