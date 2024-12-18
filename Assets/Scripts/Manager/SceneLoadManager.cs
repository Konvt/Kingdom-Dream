using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

//用于管理场景加载
public class SceneLoadManager : MonoBehaviour 
{
    public FadePanel fadePanel;

    public AssetReference Map; //获取地图场景资源
    public AssetReference currentScene; //保存当前场景
    public AssetReference start; //开始场景

    public GameObject player;
    [Header("广播")]
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

    //异步操作加载场景
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
    //异步操作卸载当前激活场景

    private async Awaitable UnloadSceneTask()
    {
        fadePanel.FadeIn(0.4f);
        await Awaitable.WaitForSecondsAsync(0.4f);
        await Awaitable.FromAsyncOperation( SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene()));
    }
    //异步操作加载地图场景
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
