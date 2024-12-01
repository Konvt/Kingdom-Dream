using UnityEngine;
using UnityEngine.AddressableAssets;

public class BootScene : MonoBehaviour
{
    public AssetReference persistent;  
    private void Awake()
    {
        Addressables.LoadSceneAsync(persistent);
    }
}
