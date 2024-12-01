using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectCardPanel : MonoBehaviour
{
    private VisualElement rootElment;

    private VisualElement container;

    public VisualTreeAsset cardTemplate;

    private CardDataSO currentCardData;

    public CardManager cardManager;

    private List<Button> buttonList=new();

    private Button confirmButton;

    [Header("事件广播")]
    public ObjectEventSO finishSelectCard;

    private void OnEnable()
    {

        rootElment = GetComponent<UIDocument>().rootVisualElement;

        container = rootElment.Q<VisualElement>("Container");

        confirmButton = rootElment.Q<Button>("ConfirmButton");

        confirmButton.clicked += OnConfirmButtonclicked;

        for (int i = 0; i < 3; i++)
        {
            var card = cardTemplate.Instantiate();

            var cardData = cardManager.GetNewCardData();


            InitCard(card, cardData);

            var cardButton = card.Q<Button>("Card");

            container.Add(card);

            buttonList.Add(cardButton);

            cardButton.clicked += ()=>OnCardButtonclicked(cardButton,cardData);


        }
    }

    private void OnConfirmButtonclicked()
    {
   
            cardManager.AddOneCard(currentCardData);

        finishSelectCard.RiseEvent(null, this);
       
    }

    private void OnCardButtonclicked(Button button,CardDataSO cardData)
    {
        currentCardData = cardData;

        for (int i = 0; i < buttonList.Count; i++)
        {
            if (button == buttonList[i]) buttonList[i].SetEnabled(false);
            else buttonList[i].SetEnabled(true);    
        }
    }

    private void OnDisable()
    {
        container.Clear();
        buttonList.Clear();
    }

    public void InitCard(VisualElement card ,CardDataSO cardData)
    {

        var cardCost = card.Q<Label>("CostLabel");
        var sprite = card.Q<VisualElement>("Sprite");
        var cardType = card.Q<Label>("TypeLabel");
        var cardDescription = card.Q<Label>("DescriptionLabel");

        sprite.style.backgroundImage = new StyleBackground(cardData.cardImage);
        cardCost.text = cardData.cost.ToString();
        cardDescription.text = cardData.cardDescription.ToString();
        switch (cardData.cardType)
        {
            case CardType.Attack:
                cardType.text = "攻击";
                break;
            case CardType.Defense:
                cardType.text = "防御";
                break;
            case CardType.Abilities:
                cardType.text = "能力";

                break;
        }
    }
}
