using System;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectRewardPanel : MonoBehaviour
{
    public ObjectEventSO addOneMana;
    public ObjectEventSO addOneDrawCard;
    public ObjectEventSO finishSelectRewardPanel;

    private VisualElement rootElement;
    private Button addOneManaButton;
    private Button addOneDrawCardButton;
    private Button confirmButton;
    private void OnEnable()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;
        addOneManaButton = rootElement.Q<Button>("AddOneMana");
        addOneDrawCardButton = rootElement.Q<Button>("AddOneDrawCard");
        confirmButton = rootElement.Q<Button>("ConfirmButton");

        addOneManaButton.clicked += OnAddOneManaButtonClicked;
        addOneDrawCardButton.clicked += OnAddOneDrawCardButtonClicked;
        confirmButton.clicked += OnConfirmButtonclicked;


    }

    private void OnConfirmButtonclicked()
    {
        finishSelectRewardPanel.RiseEvent(null, this);
    }

    private void OnAddOneDrawCardButtonClicked()
    {
        addOneDrawCard.RiseEvent(null, this);
    }

    private void OnAddOneManaButtonClicked()
    {
        addOneMana.RiseEvent(null, this);
    }
}
