using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;
//管理抽牌弃牌洗牌
public class CardDeck : MonoBehaviour
{
    public CardManager cardManager;
    public CardLayoutManager cardLayoutManager;
    private List<CardDataSO> drawDeck = new(); //抽牌堆
    private List<CardDataSO> discardDeck = new(); //弃牌堆
    private List<Card> handCardObjectList = new(); //当前手牌(每回合)

    public Vector3 deckPosition; //抽牌动画起始的位置

    //测试用
    [Header("事件广播")]
    public IntEventSO drawCountEvent;
    public IntEventSO discardCountEvent;
    private void Start()
    {
        InitializeDeck();
    }
    public void InitializeDeck()
    {
        //将牌库添加进抽牌堆
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
    [ContextMenu("TestDrawCard")] //测试用
    public void TestDrawCard()
    {
        DrawCard(1);
    }

    public void NewTurnDrawCards()
    {
        DrawCard(5);
    }
    //实现抽牌
    public void DrawCard(int amount)  
    {
        for (int i = 0; i < amount; i++)
        {
            if (drawDeck.Count == 0) //抽牌堆没有牌
            {
                foreach (var card_ in discardDeck)
                {
                    drawDeck.Add(card_);
                }
                ShuffleDeck();
            }
            //抽出抽牌堆顶部的牌
            CardDataSO currentCardData = drawDeck[0];
            drawDeck.RemoveAt(0);

            drawCountEvent.RiseEvent(drawDeck.Count, this);
            var card = cardManager.GetCardObject().GetComponent<Card>();
            card.Init(currentCardData);

            card.transform.position = deckPosition;

            handCardObjectList.Add(card); 

            var delay =i* 0.2f; //抽牌延时，实现一张一张抽的效果

            SetCardLayout(delay);


        }
    }
    private void SetCardLayout(float delay) //设置卡牌布局
    {
        for (int i = 0; i < handCardObjectList.Count; i++)
        {
            //调用cardLayoutManager获取每个卡牌的位置
            Card currentCard = handCardObjectList[i];
            CardTransform cardTransform= cardLayoutManager.GetCardTransform(i, handCardObjectList.Count);

            currentCard.UpdateCardState(); 
            //抽牌动画
            currentCard.isAnimating = true;
            currentCard.transform.DOScale(Vector3.one,0.5f).SetDelay(delay).onComplete=()=> {
                currentCard.transform.DOMove(cardTransform.position, 0.5f).onComplete=()=> currentCard.isAnimating = false ;
                currentCard.transform.DORotateQuaternion(cardTransform.rotation, 0.5f);
            };
            //设置卡牌显示序号
            currentCard.GetComponent<SortingGroup>().sortingOrder = i;

            currentCard.UpdatePositionRotation(cardTransform.position, cardTransform.rotation);
        }
        
        
    }

    //洗牌函数
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
    //弃牌逻辑
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
