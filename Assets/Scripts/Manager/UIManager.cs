using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Ãæ°å")]

    public GameObject gameplayPanel;
    public GameObject gameWinPanel;
    public GameObject gameoverPanel;
    public GameObject selectCardPanel;
    public GameObject RestRoomPanel;


    public void OnLoadRoomEvent(object data)
    { 
        Room room = data as Room;
        switch (room.roomData.roomType)
        {
            case RoomType.MinorEnemy:
            case RoomType.EliteEnemy:
            case RoomType.Boss:
                gameplayPanel.SetActive(true);
                break;
            case RoomType.Shop:
                break;
            case RoomType.Treasure:
                break;
            case RoomType.RestRoom:    
                RestRoomPanel.SetActive(true);
                break;

        }
    }
    public void HideAllPanel()
    {
        gameplayPanel.SetActive(false);
        gameWinPanel.SetActive(false);
        gameoverPanel.SetActive(false);
        RestRoomPanel.SetActive(false);

    }
    public void OnGameWinEvent()
    {
        gameplayPanel.SetActive(false);
        gameWinPanel.SetActive(true);
    }
    public void OnGameOverEvent() 
    {
        gameplayPanel.SetActive(false);
        gameoverPanel.SetActive(true);
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().hp.currentValue <= 0)
        {
            gameoverPanel.GetComponent<GameoverPanel>().DisableNextStage();
        }
    }
    public void OnSelectCardEvent()
    {
        selectCardPanel.SetActive(true);
    }

    public void OnFinishSelectCard()
    {
        selectCardPanel.SetActive(false);

    }
}
