using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class GameWinPanel : MonoBehaviour
{
    private VisualElement rootElement;

    private Button selectCardButton;
    private Button backToMapButton;

    [Header("ÊÂ¼þ¹ã²¥")]
    public ObjectEventSO backToMapEvent;
    public ObjectEventSO selectCardEvent;

    private void OnEnable()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;
        selectCardButton = rootElement.Q<Button>("SelectCardsButton");
        backToMapButton = rootElement.Q<Button>("BackToMapButton");

        backToMapButton.clicked += OnBackToMapButtonclicked;

        selectCardButton.clicked += OnSelectCardButtonclicked;
    }

    private void OnSelectCardButtonclicked()
    {
        selectCardEvent.RiseEvent(null, this);
    }

    private void OnBackToMapButtonclicked()
    {
        backToMapEvent.RiseEvent(null, this);
    }

    public void OnFinishSelectCard()
    {
        selectCardButton.style.display = DisplayStyle.None;
    }
}
