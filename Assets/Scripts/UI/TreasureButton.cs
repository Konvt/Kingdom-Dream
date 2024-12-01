using UnityEngine;
using UnityEngine.EventSystems;

public class TreasureButton : MonoBehaviour, IPointerDownHandler
{
    [Header("�¼��㲥")]
    public ObjectEventSO gameWinEvent;
    public void OnPointerDown(PointerEventData eventData)
    {
        gameWinEvent.RiseEvent(null, this);
    }
}
