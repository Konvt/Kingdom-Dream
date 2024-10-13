using UnityEngine;
using UnityEngine.EventSystems;

//处理卡牌拖拽
public class CardDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool canMove; //卡牌能否移动
    public bool canExecute; //卡牌能否被执行
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
    //开始拖拽的逻辑
    public void OnBeginDrag(PointerEventData eventData)
    {
        switch (currentCard.cardData.cardType)
        {
            //攻击生成攻击箭头
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
    //处理拖拽中的逻辑
    public void OnDrag(PointerEventData eventData)
    {
        if (canMove)//如何卡牌可以被移动，更新卡牌的实时坐标
        {
            currentCard.isAnimating = true;
            Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            currentCard.transform.position = worldPos;

            //当非攻击牌拖拽到y>1的区域时代表可以执行
            if (transform.position.y > 1f) canExecute = true;
        }
        else //不能移动（攻击牌）
        {
            //没有找到物体
            if (eventData.pointerEnter == null) return;

            //找到了敌人
            if (eventData.pointerEnter.CompareTag("Enemy"))
            {
                canExecute = true;
                targetCharacter = eventData.pointerEnter.GetComponent<CharacterBase>();
                return;
            }
            //没有找到Enemy
            canExecute = false;
            targetCharacter = null;
        }
    }

    //拖拽结束逻辑
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
