using UnityEngine;
using UnityEngine.EventSystems;

//´¦Àí¿¨ÅÆÍÏ×§
public class CardDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool canMove;
    public bool canExecute;
    private Card currentCard;

    public GameObject arrowPrefab;
    public GameObject currentArrow;


    private void Awake()
    {
        currentCard = GetComponent<Card>();

       
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        switch (currentCard.cardData.cardType)
        {
            case CardType.Attack:
                currentArrow = Instantiate(arrowPrefab,transform.position,Quaternion.identity);
                break;
            case CardType.Defense:
            case CardType.Abilities:
                canMove = true;
                break;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canMove)
        {
            currentCard.isAnimating = true;
            Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            currentCard.transform.position = worldPos;

            if (transform.position.y > 0f) canExecute = true;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(currentArrow!=null) Destroy(currentArrow);
        if (canExecute)
        { 

        }
        else
        {
            currentCard.isAnimating = false;

            currentCard.ResetCardTransform();
        }
     
    }


}
