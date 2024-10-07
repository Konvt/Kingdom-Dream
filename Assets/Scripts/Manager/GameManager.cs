using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("��ͼ����")]
    public MapLayoutSO mapLayoutSO; //��ͼ��������

    public void UpdateMaplayoutData(object value) //������뷿��󷿼�״̬���߼�
    {

        var roomVector = (Vector2Int)value;
        var currentRoom = mapLayoutSO.mapRoomDataList.Find(r => r.column == roomVector.x && r.line == roomVector.y);

        currentRoom.roomState = RoomState.visitied;

        var sameColumnRoom = mapLayoutSO.mapRoomDataList.FindAll(r => r.column == roomVector.x);

        foreach (var room in sameColumnRoom)
        {
            if (room.line != roomVector.y)
            {
                room.roomState = RoomState.locked;
            }
        }
        foreach (var link in currentRoom.linkTo)
        {
            var linkRoom = mapLayoutSO.mapRoomDataList.Find(r => r.column == link.x && r.line == link.y);
            linkRoom.roomState = RoomState.attainable;
        }

    }
}
