using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="MapLayoutSO",menuName = "Map/MapLayoutSO")]
public class MapLayoutSO : ScriptableObject //存储地图数据
{

    public List<MapRoomData> mapRoomDataList = new(); //房间数据
    public List<LinePosition> linePositionList = new(); //连线数据
}

[System.Serializable]

public class MapRoomData 
{
    public float posX, posY;
    public int column, line;
    public RoomDataSO roomData;
    public RoomState roomState;
    public List<Vector2Int> linkTo;
}

[System.Serializable]
public class LinePosition
{
    public SerializeVector3 startPos, endPos;
}