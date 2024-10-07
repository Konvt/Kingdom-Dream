using System;
using System.Collections.Generic;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CardManager : MonoBehaviour
{
    public PoolTool poolTool; //对象池
    public List<CardDataSO> cardDataList;  //存储游戏中所有卡牌
    [Header("卡牌库")]
    public CardLibrarySO newGameCardLibrary;//初始卡牌库

    public CardLibrarySO currentCardLibrary;//当前玩家牌库
    private void Awake()
    {
        InitializeCardDataList(); //获取全部卡牌

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
    private void InitializeCardDataList()
    {
        //在文件夹中查询卡牌数据资源
        Addressables.LoadAssetsAsync<CardDataSO>("CardData", null).Completed += OnCardDataLoaded;
    }

    //自动读取游戏内卡牌的数据文件
    private void OnCardDataLoaded(AsyncOperationHandle<IList<CardDataSO>> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            cardDataList = new List<CardDataSO>(handle.Result);
        }
        else
        {
            Debug.LogError("No CardDataSO found");
        }
    }
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
}
