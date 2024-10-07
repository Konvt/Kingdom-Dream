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

    [Header("原始数据")]
    public Vector3 originalPosition;
    public quaternion originalRotation;
    public int originalLayOrder;

    public bool isAnimating;
    private void Start()
    {
        Init(cardData);
    }
    public void  Init(CardDataSO data )
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

    public void UpdatePositionRotation(Vector3 position, Quaternion rotation)
    {
        originalPosition = position;
        originalRotation = rotation;
        originalLayOrder = GetComponent<SortingGroup>().sortingOrder;
    }

    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isAnimating) return;
        transform.position = originalPosition + Vector3.up;
        transform.rotation = quaternion.identity;
        GetComponent<SortingGroup>().sortingOrder = 20;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isAnimating) return;
        ResetCardTransform();

    }
    public void ResetCardTransform()
    {
        transform.position=originalPosition;
        transform.rotation = quaternion.identity;
        GetComponent<SortingGroup>().sortingOrder = originalLayOrder;
    }
}
