using UnityEngine;
using UnityEngine.UIElements;

public class RestRoomPanel : MonoBehaviour
{
    public Effect restEffect;

    private VisualElement rootElement;

    private Button restButton;
    private Button backToMapButton;

    private CharacterBase player;

    [Header("ÊÂ¼þ¹ã²¥")]
    public ObjectEventSO backToMapEvent;
    private void OnEnable()
    {
        player = FindAnyObjectByType<Player>(FindObjectsInactive.Include);
        rootElement = GetComponent<UIDocument>().rootVisualElement;

        restButton = rootElement.Q<Button>("RestButton");
        backToMapButton = rootElement.Q<Button>("BackToMapButton");

        restButton.clicked += OnRestButtonclicked;
        backToMapButton.clicked += OnBacktoMapButtonclicked;

    }

    private void OnBacktoMapButtonclicked()
    {
        backToMapEvent.RiseEvent(null, this);
    }

    private void OnRestButtonclicked()
    {
        restEffect.Excute(null, player);
        restButton.SetEnabled(false);
    }
}
