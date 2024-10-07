using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="MapConfigSO",menuName ="Map/MapConfigSO")]
//本地地图配置，用于设置地图有几列，每列的最大最小数量以及房间类型
public class MapConfigSO : ScriptableObject
{
    public List<RoomBlueprint> roomBlueprints;
}
[System.Serializable]
public class RoomBlueprint
{
    public int min,max;
    public RoomType roomType;
}