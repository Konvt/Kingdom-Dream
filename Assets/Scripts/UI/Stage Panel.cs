using UnityEngine;
using UnityEngine.UIElements;

public class StagePanel : MonoBehaviour
{

    private Label stageNumber;

    [Header("сно╥╫в╤н")]
    public IntVariable stage;
    private void OnEnable()
    {
        stageNumber = GetComponent<UIDocument>().rootVisualElement.Q<Label>("StageNumber");

        stageNumber.text = stage.currentValue.ToString();
    }

 
}
