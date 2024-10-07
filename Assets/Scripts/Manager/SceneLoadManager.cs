using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

//���ڹ���������
public class SceneLoadManager : MonoBehaviour 
{
    public AssetReference Map; //��ȡ��ͼ������Դ
    public AssetReference currentScene; //���浱ǰ����
    [Header("�㲥")]
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

    //�첽�������س���
    private async Awaitable LoadSceneTask() 
    {
        var s = currentScene.LoadSceneAsync(LoadSceneMode.Additive);

        await s.Task;

        if (s.Status == AsyncOperationStatus.Succeeded)
        {
            SceneManager.SetActiveScene(s.Result.Scene);
        }

    }
    //�첽����ж�ص�ǰ�����

    private async Awaitable UnloadSceneTask()
    {
        await SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }
    //�첽�������ص�ͼ����
    public async void LoadMap()
    {
        await UnloadSceneTask();

        currentScene = Map;

        await LoadSceneTask();
    }
}
