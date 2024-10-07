using UnityEngine;

[System.Serializable]
public class SerializeVector3 //可以序列化的vector3 类型
{
    public float x, y, z;

    public SerializeVector3(Vector3 pos)
    {
        x=pos.x; y=pos.y; z=pos.z;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(x,y,z);
    }

    public Vector2Int ToVector2Int()
    {
        return new Vector2Int((int)x, (int)y);
    }

}
