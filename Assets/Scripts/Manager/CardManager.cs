using System;
using System.Collections.Generic;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CardManager : MonoBehaviour
{
    public PoolTool poolTool; //�����
    public List<CardDataSO> cardDataList;  //�洢��Ϸ�����п���
    [Header("���ƿ�")]
    public CardLibrarySO newGameCardLibrary;//��ʼ���ƿ�

    public CardLibrarySO currentCardLibrary;//��ǰ����ƿ�
    private void Awake()
    {
        InitializeCardDataList(); //��ȡȫ������

        //������Ϸ�ĳ�ʼ���ƿ��������ǰ���ƿ�
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
        //���ļ����в�ѯ����������Դ
        Addressables.LoadAssetsAsync<CardDataSO>("CardData", null).Completed += OnCardDataLoaded;
    }

    //�Զ���ȡ��Ϸ�ڿ��Ƶ������ļ�
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
    //����һ�ſ���
    public GameObject GetCardObject()
    {
        //��ȡ����ʹ�ȳ�ʼ������Ϊ0���Ա��������չʾ
        var cardObj = poolTool.GetGameObjectFromPool();
        cardObj.transform.localScale = Vector3.zero;
        return cardObj;
    }
    //���տ���
    public void DiscardCard(GameObject cardObj)
    {
        poolTool.ReturnGameObjectToPool(cardObj);
    }
}
