using UnityEngine;

public class FinishRoom : MonoBehaviour //按钮点击事件
{

    public ObjectEventSO loadMapEvent;
    private void OnMouseDown()
    {
        loadMapEvent.RiseEvent(null, this);
    }
}
