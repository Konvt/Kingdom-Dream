using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("地图布局")]
    public MapLayoutSO mapLayoutSO; //地图布局数据

    public List<Enemy> aliveEnemyList = new();

    [Header("事件广播")]
    public ObjectEventSO gameWinEvent;
    public ObjectEventSO gameoverEvent;
    public void UpdateMaplayoutData(object value) //处理进入房间后房间状态的逻辑,保存地图数据
    {

        var roomVector = (Vector2Int)value;

        if (mapLayoutSO.mapRoomDataList.Count == 0) return;

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

        aliveEnemyList.Clear();

    }

    public void OnCharaterDeadEvnet(object character) //角色死亡事件
    {
        if (character is Player)
        {

            StartCoroutine(EventDelayAction(gameoverEvent));
        }
        else if (character is Boss)
        {
            StartCoroutine(EventDelayAction(gameoverEvent));
        }
        else if (character is Enemy)
        {
            aliveEnemyList.Remove(character as Enemy);
            if (aliveEnemyList.Count == 0)
            {
                StartCoroutine(EventDelayAction(gameWinEvent));

            }
        }
        

    }

    public void OnRoomLoadedEvent() //房间加载完成后找到所有敌人
    {
        var enimies = FindObjectsByType<Enemy>(FindObjectsInactive.Include,FindObjectsSortMode.None);

        foreach (var enemy in enimies) aliveEnemyList.Add(enemy);
    }

    IEnumerator EventDelayAction(ObjectEventSO eventSO)
    {
        yield return new WaitForSeconds(1.5f);

        eventSO.RiseEvent(this, null);
    }

    public void NewGame()
    {
        mapLayoutSO.linePositionList.Clear();
        mapLayoutSO.mapRoomDataList.Clear();
    }
}
