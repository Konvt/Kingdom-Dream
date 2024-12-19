using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DiscardPanel : MonoBehaviour
{
    private VisualElement rootElment;

    private VisualElement container;

    public VisualTreeAsset cardTemplate;

    private CardDataSO currentCardData;

    public CardManager cardManager;

    private List<Button> buttonList = new();

    private Button confirmButton;

    [Header("事件广播")]
    public ObjectEventSO finishDiscardCard;

    private void OnEnable()
    {

        rootElment = GetComponent<UIDocument>().rootVisualElement;

        container = rootElment.Q<VisualElement>("Container");

        confirmButton = rootElment.Q<Button>("ConfirmButton");

        confirmButton.clicked += OnConfirmButtonclicked;

        for (int i = 0; i < cardManager.currentCardLibrary.cardLibraryList.Count; i++)
        {
            var card = cardTemplate.Instantiate();

            CardDataSO cardData = cardManager.currentCardLibrary.cardLibraryList[i].cardData;
            InitCard(card, cardData, cardManager.currentCardLibrary.cardLibraryList[i].amount);

            var cardButton = card.Q<Button>("Card");

            container.Add(card);

            buttonList.Add(cardButton);

            cardButton.clicked += () => OnCardButtonclicked(cardButton, cardData);


        }
    }

    private void OnConfirmButtonclicked()
    {

        cardManager.DiscardOneCard(currentCardData);

        finishDiscardCard.RiseEvent(null, this);

    }

    private void OnCardButtonclicked(Button button, CardDataSO cardData)
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

    public void InitCard(VisualElement card, CardDataSO cardData,int amount)
    {

        var cardCost = card.Q<Label>("CostLabel");
        var sprite = card.Q<VisualElement>("Sprite");
        var cardType = card.Q<Label>("TypeLabel");
        var cardDescription = card.Q<Label>("DescriptionLabel");
        var name = card.Q<Label>("CardName");
        var count = card.Q<Label>("Amount");
        sprite.style.backgroundImage = new StyleBackground(cardData.cardImage);
        cardCost.text = cardData.cost.ToString();
        cardDescription.text = cardData.cardDescription.ToString();
        name.text = cardData.name.ToString();
        count.text = ("×" + amount).ToString();
        count.style.display = DisplayStyle.Flex;
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
