using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class Card : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [Header("组件")] 
    public SpriteRenderer cardSprite; 

    public TextMeshPro costText, descriptionText, typeText;

    public CardDataSO cardData;

    [Header("原始数据")] //原始数据，用于动画效果后恢复原始位置
    public Vector3 originalPosition;
    public quaternion originalRotation;
    public int originalLayOrder;

    public bool isAnimating;
    private void Start()
    {
        Init(cardData);
    }
    public void  Init(CardDataSO data ) //根据卡牌数据初始化卡牌
    {
        this.cardData = data;
        cardSprite.sprite = data.cardImage;
        costText.text = data.cost.ToString();
        descriptionText.text =data.cardDescription;
        typeText.text = data.cardType switch //unity自带字体不支持中文，需要下载字体支持中文
        {
            CardType.Attack => "Attack",
            CardType.Defense => "Defense",
            CardType.Abilities => "Abilitiese",
            _ => throw new System.NotImplementedException(),
        };
    }
    //卡牌坐标生成后，赋值给原始坐标
    public void UpdatePositionRotation(Vector3 position, Quaternion rotation)
    {
        originalPosition = position;
        originalRotation = rotation;
        originalLayOrder = GetComponent<SortingGroup>().sortingOrder;
    }

    //鼠标进入事件,卡牌实现抽出效果
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isAnimating) return;
        transform.position = originalPosition + Vector3.up;
        transform.rotation = quaternion.identity;
        GetComponent<SortingGroup>().sortingOrder = 20;
    }
    //鼠标移出事件
    public void OnPointerExit(PointerEventData eventData)
    {
        if (isAnimating) return;
        ResetCardTransform();

    }
    //恢复卡牌原始位置
    public void ResetCardTransform()
    {
        transform.position=originalPosition;
        transform.rotation = quaternion.identity;
        GetComponent<SortingGroup>().sortingOrder = originalLayOrder;
    }
}
