using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

//用于管理场景加载
public class SceneLoadManager : MonoBehaviour 
{
    public AssetReference Map; //获取地图场景资源
    public AssetReference currentScene; //保存当前场景
    [Header("广播")]
    public ObjectEventSO afterRoomLoadedEvent;

    private Vector2Int currentRoomVector;
    public async void OnLoadRoomEvent(object data)
    {
        if (data is Room)
        {
            var currentRoom = data as Room;

            var currentData = currentRoom.roomData;

            currentRoomVector = new Vector2Int(currentRoom.column, currentRoom.line);

            currentScene = currentData.sceneToload;
        }

        await UnloadSceneTask();


        await LoadSceneTask();

        afterRoomLoadedEvent.RiseEvent(currentRoomVector, this);

    }

    //异步操作加载场景
    private async Awaitable LoadSceneTask() 
    {
        var s = currentScene.LoadSceneAsync(LoadSceneMode.Additive);

        await s.Task;

        if (s.Status == AsyncOperationStatus.Succeeded)
        {
            SceneManager.SetActiveScene(s.Result.Scene);
        }

    }
    //异步操作卸载当前激活场景

    private async Awaitable UnloadSceneTask()
    {
        await SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }
    //异步操作加载地图场景
    public async void LoadMap()
    {
        await UnloadSceneTask();

        currentScene = Map;

        await LoadSceneTask();
    }
}
