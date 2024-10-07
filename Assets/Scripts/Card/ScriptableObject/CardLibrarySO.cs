using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardLibrarySO",menuName = "Card/CardLibrarySO")]
//¿¨ÅÆ¿âÊý¾Ý
public class CardLibrarySO : ScriptableObject
{
    public List<CardLibraryEntry> cardLibraryList; 
}

[System.Serializable]

//´æ´¢¿¨ÅÆ+ÊýÁ¿
public struct CardLibraryEntry
{
    public CardDataSO cardData;

    public int amount;
}

