using UnityEngine;
using UnityEngine.UIElements;

public class GameoverPanel : MonoBehaviour
{
    private Button back2Start;
    private Button startNextStage;
    [Header("ÊÂ¼þ¹ã²¥")]
    public ObjectEventSO backToStartEvent;
    public ObjectEventSO startNextStageEvnet;
    private void OnEnable()
    {
        back2Start = GetComponent<UIDocument>().rootVisualElement.Q<Button>("BakcToStartButton");

        back2Start.clicked += OnBackToStartButtonclicked;

        startNextStage = GetComponent<UIDocument>().rootVisualElement.Q<Button>("StartNextStage");
        startNextStage.clicked += OnStartNextStageButtonclicked;
    }

    private void OnStartNextStageButtonclicked()
    {
        startNextStageEvnet.RiseEvent(null, this);
    }

    private void OnBackToStartButtonclicked()
    {
        backToStartEvent.RiseEvent(null, this);
    }
}
