using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class EndlessGameChooseGainPanel : MonoBehaviour
{
    private VisualElement rootElement;

    private Button selectCardButton;
    private Button addCostLimitBotton;

    private Button addHPButton;
    private Button discardCardBotton;

    [Header("事件广播")]
    public ObjectEventSO addCostLimitEvent;
    public ObjectEventSO selectCardEvent;
    public ObjectEventSO addHPEvent;
    public ObjectEventSO discardCardEvent;
    public ObjectEventSO closeGainPanel;
    private void OnEnable()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;
        selectCardButton = rootElement.Q<Button>("ChooseCard");
        addCostLimitBotton = rootElement.Q<Button>("AddCostLimit");
        addHPButton = rootElement.Q<Button>("AddHP");
        discardCardBotton = rootElement.Q<Button>("DiscardCard");
        
        addCostLimitBotton.clicked += OnAddCostLimitButtonclicked;

        selectCardButton.clicked += OnSelectCardButtonclicked;

        addHPButton.clicked += OnAddHPButtonclicked;

        discardCardBotton.clicked += OnDiscardCardButtonclicked;
    }

    private void OnSelectCardButtonclicked()
    {
        selectCardEvent.RiseEvent(null, this);
        closeGainPanel.RiseEvent(null, this);
    }

    private void OnAddCostLimitButtonclicked()
    {
        addCostLimitEvent.RiseEvent(null, this);
        closeGainPanel.RiseEvent(null, this);

    }

    private void OnAddHPButtonclicked()
    {

        addHPEvent.RiseEvent(null, this);
        closeGainPanel.RiseEvent(null, this);

    }

    private void OnDiscardCardButtonclicked()
    {
        discardCardEvent.RiseEvent(null, this);
        closeGainPanel.RiseEvent(null, this);

    }

}
