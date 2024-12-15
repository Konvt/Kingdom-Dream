using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class EndlessGamePanel : MonoBehaviour
{
    private VisualElement rootElement;

    private Button enterEndlessButton;
    private Button backToMapButton;

    [Header("�¼��㲥")]
    public ObjectEventSO backToMapEvent;
    public ObjectEventSO selectCardEvent;

    private void OnEnable()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;
        enterEndlessButton = rootElement.Q<Button>("EnterEndlessButton");
        backToMapButton = rootElement.Q<Button>("BackToMapButton");

        backToMapButton.clicked += OnBackToMapButtonclicked;

        enterEndlessButton.clicked += OnEnterEndlessButtonclicked;
    }

    private void OnEnterEndlessButtonclicked()
    {
        selectCardEvent.RiseEvent(null, this);
    }

    private void OnBackToMapButtonclicked()
    {
        backToMapEvent.RiseEvent(null, this);
    }


}
