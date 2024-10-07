using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="MapConfigSO",menuName ="Map/MapConfigSO")]
//���ص�ͼ���ã��������õ�ͼ�м��У�ÿ�е������С�����Լ���������
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