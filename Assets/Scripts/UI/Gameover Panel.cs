using UnityEngine;
using UnityEngine.UIElements;

public class GameoverPanel : MonoBehaviour
{
    private Button back2Start;
    [Header("ÊÂ¼þ¹ã²¥")]
    public ObjectEventSO backToStartEvent;
    private void OnEnable()
    {
        back2Start = GetComponent<UIDocument>().rootVisualElement.Q<Button>("BakcToStartButton");

        back2Start.clicked += OnBackToStartButtonclicked;
    }

    private void OnBackToStartButtonclicked()
    {
        backToStartEvent.RiseEvent(null, this);
    }
}
