using UnityEngine;

public class TurnBaseManager : MonoBehaviour
{
    private bool isPlayerTurn = false;
    private bool isEnemyTurn = false;
    public bool battleEnd = true;

    private float timeCounter;
    public float playerTurnDuration;
    public float enemyTurnDuration;


    [Header("事件广播")]
    public ObjectEventSO playerTurnBegin;
    public ObjectEventSO enemyTurnBegin;
    public ObjectEventSO enemyTurnEnd;

    public GameObject player;


    private void Update()
    {
        if (battleEnd)
        {
            return;
        }
        if (isEnemyTurn)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter > enemyTurnDuration)
            {
                timeCounter = 0;
                EnemyTurnEnd();
                isPlayerTurn = true;
            }
        }
        if (isPlayerTurn)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter > playerTurnDuration) //播放动画的时间，不是回合时间
            {
                timeCounter = 0;
                PlayerTurnBegin();
                isPlayerTurn = false;
            }
        }
    }

    [ContextMenu("Game Start")]
    public void GameStart()
    {
        isPlayerTurn =true;
        isEnemyTurn = false;
        battleEnd = false;
        timeCounter = 0;
    }
    public void PlayerTurnBegin()
    {
        playerTurnBegin.RiseEvent(null, this);
    }
    public void EnemyTurnBegin()
    {
        isEnemyTurn=true;
        enemyTurnBegin.RiseEvent(null,this);    
    }
 
    public void EnemyTurnEnd()
    {
        isEnemyTurn = false;
        enemyTurnEnd.RiseEvent (null,this);
    }
    public void OnRoomLoadEvent(object obj)
    {
        Room room = obj as Room;
        switch (room.roomData.roomType)
        {
            case RoomType.MinorEnemy:
            case RoomType.EliteEnemy:
            case RoomType.Boss:
                player.SetActive(true);
                GameStart();
                break;
            case RoomType.Shop:
            case RoomType.Treasure:
                player.SetActive(false);
                break;
            case RoomType.RestRoom:
                player.SetActive(true);
                player.GetComponent<PlayerAnimationController>().SleepAnimation();
                break;

        }
    }
    public void ShutTurnBaseEvent()
    {
        battleEnd = true;
        player.SetActive (false);
    }

    public void NewGame()
    {
        player.GetComponent<Player>().NewGame();
    }
}
