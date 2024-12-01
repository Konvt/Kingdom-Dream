using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class Card : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [Header("���")] 
    public SpriteRenderer cardSprite; 

    public TextMeshPro costText, descriptionText, typeText;

    public CardDataSO cardData;

    public Player player;

    [Header("ԭʼ����")] //ԭʼ���ݣ����ڶ���Ч����ָ�ԭʼλ��
    public Vector3 originalPosition;
    public quaternion originalRotation;
    public int originalLayOrder;

    [Header("�㲥")]
    public ObjectEventSO discardCardEvent;
    public IntEventSO costEvent;

    public bool isAvailable;

    public bool isAnimating;
    private void Start()
    {
        Init(cardData);

    }
    public void  Init(CardDataSO data ) //���ݿ������ݳ�ʼ������
    {
        this.cardData = data;
        cardSprite.sprite = data.cardImage;
        costText.text = data.cost.ToString();
        descriptionText.text =data.cardDescription;
        typeText.text = data.cardType switch //unity�Դ����岻֧�����ģ���Ҫ��������֧������
        {
            CardType.Attack => "����",
            CardType.Defense => "����",
            CardType.Abilities => "����",
            _ => throw new System.NotImplementedException(),
        };
        player = GameObject.FindWithTag("Player").GetComponent<Player>();   
    }
    //�����������ɺ󣬸�ֵ��ԭʼ����
    public void UpdatePositionRotation(Vector3 position, Quaternion rotation)
    {
        originalPosition = position;
        originalRotation = rotation;
        originalLayOrder = GetComponent<SortingGroup>().sortingOrder;
    }

    //�������¼�,����ʵ�ֳ��Ч��
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isAnimating) return;
        transform.position = originalPosition + Vector3.up;
        transform.rotation = quaternion.identity;
        GetComponent<SortingGroup>().sortingOrder = 20;
    }
    //����Ƴ��¼�
    public void OnPointerExit(PointerEventData eventData)
    {
        if (isAnimating) return;
        ResetCardTransform();

    }
    //�ָ�����ԭʼλ��
    public void ResetCardTransform()
    {
        transform.position=originalPosition;
        transform.rotation = quaternion.identity;
        GetComponent<SortingGroup>().sortingOrder = originalLayOrder;
    }

    //������Ƶ�ִ���߼�
    public void EexcuteCardEffects(CharacterBase from , CharacterBase target)
    {
        costEvent.RiseEvent(cardData.cost,this);
        foreach (var effect in cardData.effects)
        {
            effect.Excute(from, target);
        }
        discardCardEvent.RiseEvent(this, this);
    }

    public void UpdateCardState()
    {
        isAvailable = player.currentMana>=cardData.cost;

        costText.color = isAvailable?Color.green : Color.red;
    }
}
