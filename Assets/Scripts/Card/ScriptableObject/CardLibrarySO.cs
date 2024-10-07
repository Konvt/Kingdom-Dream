using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardLibrarySO",menuName = "Card/CardLibrarySO")]
//���ƿ�����
public class CardLibrarySO : ScriptableObject
{
    public List<CardLibraryEntry> cardLibraryList; 
}

[System.Serializable]

//�洢����+����
public struct CardLibraryEntry
{
    public CardDataSO cardData;

    public int amount;
}

