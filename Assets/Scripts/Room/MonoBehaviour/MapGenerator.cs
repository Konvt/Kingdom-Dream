using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class MapGenerator : MonoBehaviour
{
    [Header("��ͼ���ñ�")]
    public MapConfigSO mapConfig;

    [Header("Ԥ����")]
    public LineRenderer linePrefab;
    public Room roomPrefab;

    [Header("��ͼ���ִ洢")]
    public MapLayoutSO mapLayout; //���ɵ�ͼ��洢�ĵ�ͼ����

    private float screenHeight; //��Ļ�߶�

    private float screenWidth; //��Ļ���

    public float border;  //��ͼ���ұ߽�

    public List<Room> rooms=new();  //�洢���ɵķ���
    public List<LineRenderer> lines=new(); //�洢���ɵ�����
    [Header("��ͼ���ñ�")]
    public List<RoomDataSO> roomDataList = new();

    private Dictionary<RoomType, RoomDataSO> roomDataDict=new(); //���ڽ���������ƥ���Ӧ�ķ�������

    private void Awake()
    {
        //��ȡ��Ļ�߶ȺͿ��
        screenHeight = Camera.main.orthographicSize * 2;
        screenWidth = screenHeight * Camera.main.aspect;

        //��ʼ���ֵ�
        foreach (var roomData in roomDataList)
        {
            roomDataDict.Add(roomData.roomType, roomData);
        }
    }

    private void Start()
    {


    }
    private void OnEnable()
    {

        if (mapLayout.mapRoomDataList.Count > 0) LoadMap(); //������ɹ���ͼ,����ص�ͼ
        else CreatMap(); //û�����ɹ���ͼ
    }
    public void CreatMap()
    {
        List<Room> previousRooms= new(); //����ǰһ�еķ���
        for (int column = 0; column < mapConfig.roomBlueprints.Count; column++) 
        {
            var blueprint = mapConfig.roomBlueprints[column];

            var amount = UnityEngine.Random.Range(blueprint.min, blueprint.max);

            var xGap = (screenWidth-2*border) / (mapConfig.roomBlueprints.Count-1);
            var yGap = screenHeight / (amount+1);

            //ÿ�п�ʼ������λ��
            Vector3 startPosition = new Vector3(-screenWidth /2+border+column*xGap , screenHeight / 2, 0);

            var newPosition = startPosition;

            //���浱ǰ�з���
            List<Room> currentRooms = new();

            for (int i = 0; i < amount; i++)
            {
                //ÿ�е�ÿ�������ƫ��Ч��
                float offset = 0;
                //����ÿ�����������
                if (column == 0)
                {
                    
                }
                else if (column == mapConfig.roomBlueprints.Count - 1)
                {
                    newPosition.x = screenWidth / 2 - border;
                }
                newPosition.y-= yGap; 
                newPosition.x = startPosition.x + offset;

                var room = Instantiate(roomPrefab,newPosition,Quaternion.identity,transform);
                //��ȡ��������
                RoomType roomType = GetRandomRoomType(mapConfig.roomBlueprints[column].roomType);
                //��ȡ��������,���ɶ�Ӧ����
                RoomDataSO roomData = GetRoomData(roomType);

                if (column == 0)
                {
                    room.roomState = RoomState.attainable;
                }
                else 
                {
                    room.roomState = RoomState.locked;
                }

                room.SetupRoom(column, i, roomData);

                rooms.Add(room);
                currentRooms.Add(room);
            }
            if (previousRooms.Count > 0) //��������
            {

                CreatConnections(previousRooms, currentRooms);
            }
            previousRooms = currentRooms;
        }
        SaveMap();
    }
    private RoomType GetRandomRoomType(RoomType flags) //��ȡ����ķ�������
    {
        string[] types = flags.ToString().Split(',');
        string randomType = types[UnityEngine.Random.Range(0, types.Length)];

        RoomType roomType = (RoomType)Enum.Parse(typeof(RoomType), randomType);

        return roomType;
    }
    private RoomDataSO GetRoomData(RoomType roomType) 
    {
        return roomDataDict[roomType];
    }
    private void CreatConnections(List<Room> column1, List<Room> column2) //�������з���֮������
    {
        HashSet<Room> connectedColumn2Rooms = new(); //�����Ѿ����ߵĵڶ��еķ���

        foreach (var room in column1) //������һ�з������ӵ��ڶ����������
        {
            Room target = ConnectToRandomRoom(room,column2,false); 
            connectedColumn2Rooms.Add(target);
        }

        foreach (var room in column2) //����Ƿ����û�����ߵĵڶ��еķ���
        {
            if (!connectedColumn2Rooms.Contains(room)) //�������������һ�з���
            {
                ConnectToRandomRoom(room, column1,true);
            }
        }
    }

    private Room ConnectToRandomRoom(Room room, List<Room> column2,bool check) //�����������
    {
        Room target;

        target = column2[UnityEngine.Random.Range(0, column2.Count)];
        if (check) //�Ƿ�������
        {
            target.linkTo.Add(new(room.column, room.line));
        }
        else //���Ƿ�������
        {
            room.linkTo.Add(new(target.column, target.line));
        }
        //��������
        var line = Instantiate(linePrefab,transform);
        line.SetPosition(0, room.transform.position);
        line.SetPosition(1, target.transform.position);
        lines.Add(line);
        return target;

    }

    private void SaveMap() //�����ͼ
    {
        mapLayout.mapRoomDataList = new();

        foreach (var room in rooms)
        {
            var savedRoom = new MapRoomData()
            {
                posX = room.transform.position.x,
                posY = room.transform.position.y,
                line = room.line,
                column=room.column,
                roomData =room.roomData,
                roomState =room.roomState,
                linkTo = room.linkTo,
            };
            mapLayout.mapRoomDataList.Add(savedRoom);
        }

        mapLayout.linePositionList = new();

        foreach (var line in lines)
        {
            var savedLine = new LinePosition()
            {
                startPos = new SerializeVector3(line.GetPosition(0)),
                endPos = new SerializeVector3(line.GetPosition(1)),
            };

            mapLayout.linePositionList.Add(savedLine);
        }
    }

    private void LoadMap() //���ص�ͼ
    {
        foreach (var roomData in mapLayout.mapRoomDataList)
        {
            Vector3 newPos = new Vector3(roomData.posX, roomData.posY, 0);
            var room = Instantiate(roomPrefab,newPos,Quaternion.identity,transform);
            room.roomState = roomData.roomState;
            room.linkTo = roomData.linkTo;
            room.SetupRoom(roomData.column, roomData.line, roomData.roomData);
            rooms.Add(room); 
        }

        foreach (var lineData in mapLayout.linePositionList)
        {
            Vector3 startPos = lineData.startPos.ToVector3();
            Vector3 endPos = lineData.endPos.ToVector3();

            var line = Instantiate(linePrefab, transform);
            line.SetPosition(0, startPos);
            line.SetPosition(1, endPos);

            lines.Add(line);
        }
    }
    [ContextMenu(itemName: "RegenerateRooms")]
    public void RegenerateRooms() //�������ɵ�ͼ
    {
        foreach (var room in rooms)
        {
            Destroy(room.gameObject);
        }
        foreach (var line in lines)
        {
            Destroy(line.gameObject);
        }
        rooms.Clear();
        lines.Clear();

        CreatMap();
    }
}
