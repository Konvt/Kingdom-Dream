using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class MapGenerator : MonoBehaviour
{
    [Header("地图配置表")]
    public MapConfigSO mapConfig;

    [Header("预制体")]
    public LineRenderer linePrefab;
    public Room roomPrefab;

    [Header("地图布局存储")]
    public MapLayoutSO mapLayout; //生成地图后存储的地图数据

    private float screenHeight; //屏幕高度

    private float screenWidth; //屏幕宽度

    public float border;  //地图左右边界

    public List<Room> rooms=new();  //存储生成的房间
    public List<LineRenderer> lines=new(); //存储生成的连线
    [Header("地图配置表")]
    public List<RoomDataSO> roomDataList = new();

    private Dictionary<RoomType, RoomDataSO> roomDataDict=new(); //用于将房间类型匹配对应的房间数据

    private void Awake()
    {
        //获取屏幕高度和宽度
        screenHeight = Camera.main.orthographicSize * 2;
        screenWidth = screenHeight * Camera.main.aspect;

        //初始化字典
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

        if (mapLayout.mapRoomDataList.Count > 0) LoadMap(); //如果生成过地图,则加载地图
        else CreatMap(); //没有生成过地图
    }
    public void CreatMap()
    {
        List<Room> previousRooms= new(); //保存前一列的房间
        for (int column = 0; column < mapConfig.roomBlueprints.Count; column++) 
        {
            var blueprint = mapConfig.roomBlueprints[column];

            var amount = UnityEngine.Random.Range(blueprint.min, blueprint.max);

            var xGap = (screenWidth-2*border) / (mapConfig.roomBlueprints.Count-1);
            var yGap = screenHeight / (amount+1);

            //每列开始的坐标位置
            Vector3 startPosition = new Vector3(-screenWidth /2+border+column*xGap , screenHeight / 2, 0);

            var newPosition = startPosition;

            //保存当前列房间
            List<Room> currentRooms = new();

            for (int i = 0; i < amount; i++)
            {
                //每列的每个房间的偏移效果
                float offset = 0;
                //调整每个房间的坐标
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
                //获取房间类型
                RoomType roomType = GetRandomRoomType(mapConfig.roomBlueprints[column].roomType);
                //获取房间数据,生成对应房间
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
            if (previousRooms.Count > 0) //创建连线
            {

                CreatConnections(previousRooms, currentRooms);
            }
            previousRooms = currentRooms;
        }
        SaveMap();
    }
    private RoomType GetRandomRoomType(RoomType flags) //获取随机的房间类型
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
    private void CreatConnections(List<Room> column1, List<Room> column2) //创建两列房间之间连接
    {
        HashSet<Room> connectedColumn2Rooms = new(); //保存已经连线的第二列的房间

        foreach (var room in column1) //遍历第一列房间连接到第二列随机房间
        {
            Room target = ConnectToRandomRoom(room,column2,false); 
            connectedColumn2Rooms.Add(target);
        }

        foreach (var room in column2) //检查是否存在没有连线的第二列的房间
        {
            if (!connectedColumn2Rooms.Contains(room)) //存在则反向连向第一列房间
            {
                ConnectToRandomRoom(room, column1,true);
            }
        }
    }

    private Room ConnectToRandomRoom(Room room, List<Room> column2,bool check) //房间随机连接
    {
        Room target;

        target = column2[UnityEngine.Random.Range(0, column2.Count)];
        if (check) //是反向连接
        {
            target.linkTo.Add(new(room.column, room.line));
        }
        else //不是反向连接
        {
            room.linkTo.Add(new(target.column, target.line));
        }
        //生成连线
        var line = Instantiate(linePrefab,transform);
        line.SetPosition(0, room.transform.position);
        line.SetPosition(1, target.transform.position);
        lines.Add(line);
        return target;

    }

    private void SaveMap() //保存地图
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

    private void LoadMap() //加载地图
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
    public void RegenerateRooms() //重新生成地图
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
