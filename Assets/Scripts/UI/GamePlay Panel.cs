using System;
using UnityEngine;
using UnityEngine.UIElements;
//[DefaultExecutionOrder(-100)]
public class GamePlayPanel : MonoBehaviour
{
    private VisualElement rootElement;


    private Label energyAmountLabel, drawDeckAmountLabel, discardDeckAmountLabel,turnLabel;

    private Button endTurnButton;



    [Header("�¼��㲥")]
    public ObjectEventSO playerTurnEndEvnet;
    private void Awake()
    {

    }
    private void OnEnable()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;



        energyAmountLabel = rootElement.Q<Label>("EnergyAmount");

        drawDeckAmountLabel = rootElement.Q<Label>("DrawDeckAmount");

        discardDeckAmountLabel = rootElement.Q<Label>("DiscardDeckAmount");

        turnLabel = rootElement.Q<Label>("TurnLabel");
        endTurnButton = rootElement.Q<Button>("TurnButton");

        endTurnButton.clicked += OnEndTurnButtonClicked;
        drawDeckAmountLabel.text = "0";
        energyAmountLabel.text = "0";
        discardDeckAmountLabel.text = "0";
        turnLabel.text = "��Ϸ��ʼ";
    }

    private void OnEndTurnButtonClicked()
    {
        playerTurnEndEvnet.RiseEvent(this, null);
    }

    public void UpdateDrawDeckAmount(int amount)
    {
        drawDeckAmountLabel.text = amount.ToString();
    }
    public void UpdateDisCardDeckAmount(int amount)
    {
        discardDeckAmountLabel.text = amount.ToString();
    }

    public void UpdateEnergyAmount(int amount)
    {
        energyAmountLabel.text = amount.ToString();
    }
    public void OnEnemyTurnBegin()
    {
        endTurnButton.SetEnabled(false);
        turnLabel.text = "���˻غ�";
        turnLabel.style.color = new StyleColor(Color.red);
    }
    public void OnPlayerTurnBegin()
    {
        endTurnButton.SetEnabled(true);
        turnLabel.text = "��һغ�";
        turnLabel.style.color = new StyleColor(Color.white);
    }
}
