using UnityEngine;

[CreateAssetMenu(fileName = "IntVariable", menuName = "Variable/IntVariable")]
public class IntVariable : ScriptableObject
{
    public int maxValue;

    public int currentValue;

    public IntEventSO valueChangedEvent;

    [TextArea]
    [SerializeField]private string description;
    public void SetValue(int value)
    {
        currentValue = value;
        valueChangedEvent?.RiseEvent(value,this);
    }
}

