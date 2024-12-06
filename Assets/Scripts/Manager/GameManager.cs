using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("��ͼ����")]
    public MapLayoutSO mapLayoutSO; //��ͼ��������

    public List<Enemy> aliveEnemyList = new();

    [Header("�¼��㲥")]
    public ObjectEventSO gameWinEvent;
    public ObjectEventSO gameoverEvent;
    public ObjectEventSO loadMapEvent;

    [Header("��Ϸ�׶�")]
    public IntVariable gameStage;

    private void Awake()
    {
        gameStage.SetValue(1);
        aliveEnemyList.Clear();
    }

    public void UpdateMaplayoutData(object value) //������뷿��󷿼�״̬���߼�,�����ͼ����
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

    public void OnCharaterDeadEvnet(object character) //��ɫ�����¼�
    {
        if (character is Player)
        {
            StartCoroutine(EventDelayAction(gameoverEvent));
        }
        else if (character is Boss)
        {
            aliveEnemyList.Remove(character as Boss);
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

    public void OnRoomLoadedEvent() //���������ɺ��ҵ����е���
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
        gameStage.SetValue(1);
    }
    public void GameStageUpgrade()
    {
        int temp = gameStage.currentValue;
        temp++;
        gameStage.SetValue(temp);
    }
    public void StartNextGameStage()
    {
        aliveEnemyList.Clear();
        int curStage = gameStage.currentValue;
        NewGame();
        gameStage.SetValue(curStage);
        GameStageUpgrade();
        loadMapEvent.RiseEvent(null, this);
    }
}
