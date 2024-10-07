using UnityEngine;

public struct CardTransform 
{
    public Vector3 position;
    public Quaternion rotation;

    public CardTransform(Vector3 position, Quaternion rota)
    {
        this.position = position;
        this.rotation = rota;
    }
}
