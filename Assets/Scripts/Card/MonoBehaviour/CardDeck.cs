using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;

public class CardDeck : MonoBehaviour
{
    public CardManager cardManager;
    public CardLayoutManager cardLayoutManager;
    private List<CardDataSO> drawDeck = new(); //���ƶ�
    private List<CardDataSO> discardDeck = new(); //���ƶ�
    private List<Card> handCardObjectList = new(); //��ǰ����(ÿ�غ�)

    public Vector3 deckPosition; //���ƶ�����ʼ��λ��

    //������
    private void Start()
    {
        InitializeDeck();
        DrawCard(3);
    }
    public void InitializeDeck()
    {
        //���ƿ���ӽ����ƶ�
        drawDeck.Clear();   
        foreach(var entry in cardManager.currentCardLibrary.cardLibraryList)
        {
            for (int i = 0; i < entry.amount; i++)
            {
                drawDeck.Add(entry.cardData);
            }
        }
        ShuffleDeck();
    }
    [ContextMenu("TestDrawCard")] //������
    public void TestDrawCard()
    {
        DrawCard(1);
    }
    //ʵ�ֳ���
    private void DrawCard(int amount)  
    {
        for (int i = 0; i < amount; i++)
        {
            if (drawDeck.Count == 0) //���ƶ�û����
            {
                foreach (var card_ in discardDeck)
                {
                    drawDeck.Add(card_);
                }
                ShuffleDeck();
            }
            //������ƶѶ�������
            CardDataSO currentCardData = drawDeck[0];
            drawDeck.RemoveAt(0);
            var card = cardManager.GetCardObject().GetComponent<Card>();
            card.Init(currentCardData);

            card.transform.position = deckPosition;

            handCardObjectList.Add(card); 

            var delay =i* 0.2f; //������ʱ��ʵ��һ��һ�ų��Ч��

            SetCardLayout(delay);

        }
    }
    private void SetCardLayout(float delay) //���ÿ��Ʋ���
    {
        for (int i = 0; i < handCardObjectList.Count; i++)
        {
            //����cardLayoutManager��ȡÿ�����Ƶ�λ��
            Card currentCard = handCardObjectList[i];
            CardTransform cardTransform= cardLayoutManager.GetCardTransform(i, handCardObjectList.Count);

            //���ƶ���
            currentCard.isAnimating = true;
            currentCard.transform.DOScale(Vector3.one,0.5f).SetDelay(delay).onComplete=()=> {
                currentCard.transform.DOMove(cardTransform.position, 0.5f).onComplete=()=> currentCard.isAnimating = false ;
                currentCard.transform.DORotateQuaternion(cardTransform.rotation, 0.5f);
            };
            //���ÿ�����ʾ���
            currentCard.GetComponent<SortingGroup>().sortingOrder = i;

            currentCard.UpdatePositionRotation(cardTransform.position, cardTransform.rotation);
        }
        
        
    }

    //ϴ�ƺ���
    private void ShuffleDeck()
    {

        discardDeck.Clear();
        for (int i = 0; i < drawDeck.Count; i++)
        {
            CardDataSO temp = drawDeck[i];
            int randomIndex = Random.Range(i, drawDeck.Count);
            drawDeck[i] = drawDeck[randomIndex];
            drawDeck[randomIndex] = temp;
        }
    }

    public void DiscardCard(Card discardCard)
    {
        discardDeck.Add(discardCard.cardData);
        handCardObjectList.Remove(discardCard);
        cardManager.DiscardCard(discardCard.gameObject);

        SetCardLayout(0f);
    }
}
