using UnityEngine;

public class FinishRoom : MonoBehaviour //��ť����¼�
{

    public ObjectEventSO loadMapEvent;
    private void OnMouseDown()
    {
        loadMapEvent.RiseEvent(null, this);
    }
}
