using UnityEngine;
using UnityEngine.EventSystems;

public class TreasureButton : MonoBehaviour, IPointerDownHandler
{
    [Header("ÊÂ¼þ¹ã²¥")]
    public ObjectEventSO gameWinEvent;
    public void OnPointerDown(PointerEventData eventData)
    {
        gameWinEvent.RiseEvent(null, this);
    }
}
