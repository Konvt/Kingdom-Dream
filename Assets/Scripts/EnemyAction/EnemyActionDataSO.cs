using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyActionDataSO",menuName = "EnemyAction/EnemyActionDataSO")]
public class EnemyActionDataSO :ScriptableObject
{
    public List<EnemyAction> actions;
}

[System.Serializable]
public struct EnemyAction
{
    public Sprite sprite;
    public Effect effect;
}
  