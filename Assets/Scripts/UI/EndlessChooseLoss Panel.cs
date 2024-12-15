using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class EndlessGameChooseLossPanel : MonoBehaviour
{
    private VisualElement rootElement;

    private Button decreaseAttackButton;
    private Button decreaseHPBotton;

    [Header("�¼��㲥")]
    public ObjectEventSO decreaseAttackEvent;
    public ObjectEventSO decreaseHPEvent;

    private void OnEnable()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;
        decreaseAttackButton = rootElement.Q<Button>("DecreaseAttack");
        decreaseHPBotton = rootElement.Q<Button>("DecreaseHP");

        
        decreaseHPBotton.clicked += OnDecreaseAttackButtonclicked;

        decreaseAttackButton.clicked += OnDecreaseHPButtonclicked;
    }

    private void OnDecreaseAttackButtonclicked()
    {
        decreaseAttackEvent.RiseEvent(null, this);
    }

    private void OnDecreaseHPButtonclicked()
    {
        decreaseHPEvent.RiseEvent(null, this);
    }


}
