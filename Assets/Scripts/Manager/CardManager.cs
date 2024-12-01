using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public PoolTool poolTool; //�����
    [Header("�����ƿ�")]
    public List<CardDataSO> cardDataList;  //�洢��Ϸ�����п���
    [Header("��ʼ���ƿ�")]
    public CardLibrarySO newGameCardLibrary;//��ʼ���ƿ�
    [Header("��ǰ���ƿ�")]
    public CardLibrarySO currentCardLibrary;//��ǰ����ƿ�

    private int previousIndex=-1;
    private void Awake()
    {
       // InitializeCardDataList(); //��ȡȫ������

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
    //private void InitializeCardDataList()
    //{
    //    //���ļ����в�ѯ����������Դ
    //    Addressables.LoadAssetsAsync<CardDataSO>("CardData", null).Completed += OnCardDataLoaded;
    //}

    //�Զ���ȡ��Ϸ�ڿ��Ƶ������ļ�
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

        if (currentCardLibrary.cardLibraryList.Contains(newCard))
        {

            var target =  currentCardLibrary.cardLibraryList.Find(t => t.cardData == newCardData);
            target.amount++;

        }
        else currentCardLibrary.cardLibraryList.Add(newCard);

    }
}
