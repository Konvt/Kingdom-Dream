using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

//���ڹ���������
public class SceneLoadManager : MonoBehaviour 
{
    public FadePanel fadePanel;

    public AssetReference Map; //��ȡ��ͼ������Դ
    public AssetReference currentScene; //���浱ǰ����
    public AssetReference start; //��ʼ����

    public GameObject player;
    [Header("�㲥")]
    public ObjectEventSO afterRoomLoadedEvent;
    public ObjectEventSO updateRoomEvent;

    private Vector2Int currentRoomVector;

    Room currentRoom;
    private void Awake()
    { 
        currentRoomVector =Vector2Int.one *-1;
        LoadStartMenu();
    }
    public async void OnLoadRoomEvent(object data)
    {
        if (data is Room)
        {
            currentRoom = data as Room;

            var currentData = currentRoom.roomData;

            currentRoomVector = new Vector2Int(currentRoom.column, currentRoom.line);

            currentScene = currentData.sceneToload;
        }

        await UnloadSceneTask();


        await LoadSceneTask();

        afterRoomLoadedEvent.RiseEvent(currentRoom, this);
       

    }

    //�첽�������س���
    private async Awaitable LoadSceneTask() 
    {
        var s = currentScene.LoadSceneAsync(LoadSceneMode.Additive);

        await s.Task;

        if (s.Status == AsyncOperationStatus.Succeeded)
        {
            fadePanel.FadeOut(0.3f);
            SceneManager.SetActiveScene(s.Result.Scene);
        }

    }
    //�첽����ж�ص�ǰ�����

    private async Awaitable UnloadSceneTask()
    {
        fadePanel.FadeIn(0.4f);
        await Awaitable.WaitForSecondsAsync(0.4f);
        await Awaitable.FromAsyncOperation( SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene()));
    }
    //�첽�������ص�ͼ����
    public async void LoadMap()
    {
        await UnloadSceneTask();

        
        if(currentRoomVector!=Vector2.one*-1) updateRoomEvent.RiseEvent(currentRoomVector, this);

        currentScene = Map;

        player.SetActive(false);

        await LoadSceneTask();
    }

    public async void LoadStartMenu()
    {
        if (currentScene.IsValid())
        {
            await UnloadSceneTask();

        }

        currentScene = start;

        await LoadSceneTask();
    }
}
