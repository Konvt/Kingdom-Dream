using System.Collections.Generic;
using UnityEngine;

//房间类，用于显示相应的房间类型和处理点击事件
public class Room : MonoBehaviour
{

    public int column;
    public int line;

    private SpriteRenderer spriteRenderer;

    public RoomDataSO roomData;

    public RoomState roomState;

    public List<Vector2Int> linkTo = new(); //保存该房间连接的房间
    [Header("广播")] 
    public ObjectEventSO loadRoomEvent;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
    }

    //点击时的相应逻辑
    private void OnMouseDown()
    {
        if(roomState==RoomState.attainable)
            loadRoomEvent.RiseEvent(this,this);
    }

    //外部创建时调用
    public void SetupRoom(int column,int line,RoomDataSO roomData)
    { 
        this.column = column;
        this.line = line;
        this.roomData = roomData;

        spriteRenderer.sprite = roomData.roomIcon;

        spriteRenderer.color = roomState switch
        {
            RoomState.locked => new Color(0.5f, 0.5f, 0.5f, 1f),
            RoomState.visitied => new Color(0.5f, 0.8f, 0.5f, 0.5f),
            RoomState.attainable => Color.white,
            _ => throw new System.NotImplementedException(),
        };

    }
}
