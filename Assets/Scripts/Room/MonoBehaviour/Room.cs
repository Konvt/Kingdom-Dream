using System.Collections.Generic;
using UnityEngine;

//�����࣬������ʾ��Ӧ�ķ������ͺʹ������¼�
public class Room : MonoBehaviour
{

    public int column;
    public int line;

    private SpriteRenderer spriteRenderer;

    public RoomDataSO roomData;

    public RoomState roomState;

    public List<Vector2Int> linkTo = new(); //����÷������ӵķ���
    [Header("�㲥")] 
    public ObjectEventSO loadRoomEvent;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
    }

    //���ʱ����Ӧ�߼�
    private void OnMouseDown()
    {
        if(roomState==RoomState.attainable)
            loadRoomEvent.RiseEvent(this,this);
    }

    //�ⲿ����ʱ����
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
