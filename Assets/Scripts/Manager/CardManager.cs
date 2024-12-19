using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public PoolTool poolTool; //对象池
    [Header("所有牌库")]
    public List<CardDataSO> cardDataList;  //存储游戏中所有卡牌
    [Header("初始卡牌库")]
    public CardLibrarySO newGameCardLibrary;//初始卡牌库
    [Header("当前卡牌库")]
    public CardLibrarySO currentCardLibrary;//当前玩家牌库

    private int previousIndex=-1;
    private void Awake()
    {
       // InitializeCardDataList(); //获取全部卡牌

        //将新游戏的初始卡牌库添加至当前卡牌库
        foreach (var item in newGameCardLibrary.cardLibraryList)
        {
            currentCardLibrary.cardLibraryList.Add(item);
        }
    }

    private void OnDisable()
    {
        currentCardLibrary.cardLibraryList.Clear();
    }
    //private void InitializeCardDataList()
    //{
    //    //在文件夹中查询卡牌数据资源
    //    Addressables.LoadAssetsAsync<CardDataSO>("CardData", null).Completed += OnCardDataLoaded;
    //}

    //自动读取游戏内卡牌的数据文件
    //private void OnCardDataLoaded(AsyncOperationHandle<IList<CardDataSO>> handle)
    //{
    //    if (handle.Status == AsyncOperationStatus.Succeeded)
    //    {
    //        cardDataList = new List<CardDataSO>(handle.Result);
    //    }
    //    else
    //    {
    //        Debug.LogError("No CardDataSO found");
    //    }

    //    cardDataList.Clear();
    //    foreach (var item in newGameCardLibrary.cardLibraryList)
    //    {
    //        cardDataList.Add(item.cardData);
    //    }
    //}
    //生成一张卡牌
    public GameObject GetCardObject()
    {
        //获取卡牌使先初始化缩放为0，以便后续动画展示
        var cardObj = poolTool.GetGameObjectFromPool();
        cardObj.transform.localScale = Vector3.zero;
        return cardObj;
    }
    //回收卡牌
    public void DiscardCard(GameObject cardObj)
    {
        poolTool.ReturnGameObjectToPool(cardObj);
    }

    public CardDataSO GetNewCardData()
    {
        int randomIndex = 0;
        do
        {
            randomIndex = UnityEngine.Random.Range(0, cardDataList.Count);

        } while (randomIndex == previousIndex);

        previousIndex = randomIndex;

        return cardDataList[randomIndex];
    }

    public void AddOneCard(CardDataSO newCardData)
    {
        var newCard = new CardLibraryEntry
        {
            cardData = newCardData,
            amount = 1,
        };
        bool flag = false;
        for (int i = 0; i < currentCardLibrary.cardLibraryList.Count; i++)
        {
            if (currentCardLibrary.cardLibraryList[i].cardData.cardName == newCardData.cardName)
            {
                var updateCard = new CardLibraryEntry
                {
                    cardData = newCardData,
                    amount = currentCardLibrary.cardLibraryList[i].amount+1,
                };
                currentCardLibrary.cardLibraryList[i] = updateCard;
                flag = true;
                break;
            }
        }
        if(!flag) currentCardLibrary.cardLibraryList.Add(newCard);

    }
    public void DiscardOneCard(CardDataSO newCardData)
    {

        for (int i = 0; i < currentCardLibrary.cardLibraryList.Count; i++)
        {
            if (currentCardLibrary.cardLibraryList[i].cardData.cardName == newCardData.cardName)
            {
                var temp = currentCardLibrary.cardLibraryList[i];
                temp.amount--;
                if(temp.amount<=0) currentCardLibrary.cardLibraryList.RemoveAt(i);
                else currentCardLibrary.cardLibraryList[i] = temp;
            }
        }

    }
}
