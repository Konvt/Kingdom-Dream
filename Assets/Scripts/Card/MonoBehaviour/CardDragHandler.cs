using UnityEngine;
using UnityEngine.EventSystems;

//��������ק
public class CardDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool canMove; //�����ܷ��ƶ�
    public bool canExecute; //�����ܷ�ִ��
    private Card currentCard;

    public GameObject arrowPrefab;
    public GameObject currentArrow;

    public CharacterBase targetCharacter;

    public CharacterBase player;
    private void Awake()
    {
        currentCard = GetComponent<Card>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBase>();
    }
    //��ʼ��ק���߼�
    public void OnBeginDrag(PointerEventData eventData)
    {
        switch (currentCard.cardData.cardType)
        {
            //�������ɹ�����ͷ
            case CardType.Attack:
                canMove = false;
                currentArrow = Instantiate(arrowPrefab,transform.position,Quaternion.identity);
                break;
            case CardType.Defense:
            case CardType.Abilities:
                canMove = true;
                break;
        }
    }
    //������ק�е��߼�
    public void OnDrag(PointerEventData eventData)
    {
        if (canMove)//��ο��ƿ��Ա��ƶ������¿��Ƶ�ʵʱ����
        {
            currentCard.isAnimating = true;
            Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            currentCard.transform.position = worldPos;

            //���ǹ�������ק��y>1������ʱ�������ִ��
            if (transform.position.y > 1f) canExecute = true;
        }
        else //�����ƶ��������ƣ�
        {
            //û���ҵ�����
            if (eventData.pointerEnter == null) return;

            //�ҵ��˵���
            if (eventData.pointerEnter.CompareTag("Enemy"))
            {
                canExecute = true;
                targetCharacter = eventData.pointerEnter.GetComponent<CharacterBase>();
                return;
            }
            //û���ҵ�Enemy
            canExecute = false;
            targetCharacter = null;
        }
    }

    //��ק�����߼�
    public void OnEndDrag(PointerEventData eventData)
    {
        if(currentArrow!=null) Destroy(currentArrow);

        if (canExecute)
        {
            currentCard.EexcuteCardEffects(player,targetCharacter);
        }
        else
        {
            currentCard.isAnimating = false;

            currentCard.ResetCardTransform();
        }
    }


}
