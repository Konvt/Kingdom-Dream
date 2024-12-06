using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;
//�����������ϴ��
public class CardDeck : MonoBehaviour
{
    public CardManager cardManager;
    public CardLayoutManager cardLayoutManager;
    private List<CardDataSO> drawDeck = new(); //���ƶ�
    private List<CardDataSO> discardDeck = new(); //���ƶ�
    private List<Card> handCardObjectList = new(); //��ǰ����(ÿ�غ�)

    public Vector3 deckPosition; //���ƶ�����ʼ��λ��

    //������
    [Header("�¼��㲥")]
    public IntEventSO drawCountEvent;
    public IntEventSO discardCountEvent;
    private void Start()
    {
        InitializeDeck();
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

    public void NewTurnDrawCards()
    {
        DrawCard(5);
    }
    //ʵ�ֳ���
    public void DrawCard(int amount)  
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

            drawCountEvent.RiseEvent(drawDeck.Count, this);
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

            currentCard.UpdateCardState(); 
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
        drawCountEvent.RiseEvent(drawDeck.Count, this);
        discardCountEvent.RiseEvent(discardDeck.Count, this);
        for (int i = 0; i < drawDeck.Count; i++)
        {
            CardDataSO temp = drawDeck[i];
            int randomIndex = Random.Range(i, drawDeck.Count);
            drawDeck[i] = drawDeck[randomIndex];
            drawDeck[randomIndex] = temp;
        }
        
    }
    //�����߼�
    public void DiscardCard(object card)
    {
        Card discardCard = card as Card;
        discardDeck.Add(discardCard.cardData);
        handCardObjectList.Remove(discardCard);
        cardManager.DiscardCard(discardCard.gameObject);
        discardCountEvent.RiseEvent(discardDeck.Count, this);
        SetCardLayout(0f);
    }

    public void OnPlayerTurnEnd()
    {
        for (int i = 0; i< handCardObjectList.Count; i++)
        {
            discardDeck.Add(handCardObjectList[i].cardData);
            cardManager.DiscardCard(handCardObjectList[i].gameObject);
            
        }

        handCardObjectList.Clear();
        discardCountEvent.RiseEvent(discardDeck.Count, this);
    }

    public void RealeaseAllCards(object obj)
    {
        foreach (var card in handCardObjectList)
        {
            cardManager.DiscardCard(card.gameObject);
        }
        handCardObjectList.Clear();
        InitializeDeck();
    }
}
