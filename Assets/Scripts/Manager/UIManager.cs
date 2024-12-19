using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Ãæ°å")]

    public GameObject gameplayPanel;
    public GameObject gameWinPanel;
    public GameObject gameoverPanel;
    public GameObject selectCardPanel;
    public GameObject RestRoomPanel;
    public GameObject SelectRewardPanel;
    public GameObject DiscardPanel;

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
        var boss = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Boss>();
        if (boss!=null && boss.hp.currentValue <= 0)
        {
            SelectRewardPanel.SetActive(true);
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
    public void CloseGainPanel()
    {
        SelectRewardPanel.SetActive(false);
    }
    public void OnDiscard()
    {
        DiscardPanel.SetActive(true);
    }
    public void CloseDiscard()
    {
        DiscardPanel.SetActive(false);
    }
}
