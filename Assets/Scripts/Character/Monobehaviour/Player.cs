using UnityEngine;

public class Player : CharacterBase
{
    public IntVariable playerMana;

    public int maxMana;

    public ObjectEventSO UpdataCardState;
    public int currentMana 
    {   get => playerMana.currentValue; 
        set {   playerMana.SetValue(value); }
    }

    private void OnEnable()
    {
        playerMana.maxValue = maxMana;
        currentMana = playerMana.maxValue;

    }
    public void NewTurn()
    {
        currentMana = maxMana;
        UpdataCardState.RiseEvent(null, this);
    }
    public void UpdataMana(int cost)
    {
        var value = playerMana.currentValue - cost;
        if (value <= 0)
        {
            currentMana = 0;
        }
        else currentMana= value;

    }

    public void NewGame()
    {
        currentMana = maxMana;
        CurrentHp = maxHp;
        NewTurn();
        strengthRound.SetValue(0);
    }
}
