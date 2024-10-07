using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("地图布局")]
    public MapLayoutSO mapLayoutSO; //地图布局数据

    public void UpdateMaplayoutData(object value) //处理进入房间后房间状态的逻辑
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
