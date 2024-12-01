using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyActionDataSO",menuName = "EnemyAction/EnemyActionDataSO")]
public class EnemyActionDataSO :ScriptableObject
{
    [Header("���б�")]
    public List<EnemyAction> actions;
}

[System.Serializable]
public struct EnemyAction
{
    public Sprite sprite;
    public Effect effect;
}
  