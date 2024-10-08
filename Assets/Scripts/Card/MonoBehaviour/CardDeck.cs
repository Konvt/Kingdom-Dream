using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;

public class CardDeck : MonoBehaviour
{
    public CardManager cardManager;
    public CardLayoutManager cardLayoutManager;
    private List<CardDataSO> drawDeck = new(); //抽牌堆
    private List<CardDataSO> discardDeck = new(); //弃牌堆
    private List<Card> handCardObjectList = new(); //当前手牌(每回合)

    public Vector3 deckPosition; //抽牌动画起始的位置

    //测试用
    private void Start()
    {
        InitializeDeck();
        DrawCard(3);
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
    //实现抽牌
    private void DrawCard(int amount)  
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
