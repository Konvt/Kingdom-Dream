using UnityEngine;
using UnityEngine.Pool;
//�����

[DefaultExecutionOrder(-100)]

public class PoolTool : MonoBehaviour
{
    public GameObject objPrefab;
    public ObjectPool<GameObject> pool;

    private void Awake()
    {
        //��ʼ�������
        pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(objPrefab, transform),
            actionOnGet: (obj) => obj.SetActive(true),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck:false,
            defaultCapacity:10,
            maxSize:20
           
        ) ;

        PrefillPool(7);
    }

    private void PrefillPool(int count) //Ԥ�����ɿ���
    {
        var prefillArray = new GameObject[count];

        for (int i = 0; i < count; i++)
        {
            prefillArray[i]=pool.Get();
        }

        foreach (var item in prefillArray)
        {
            pool.Release(item);
        }
    }
    //�ⲿ���ö����
    public GameObject GetGameObjectFromPool()
    {
        return pool.Get();  
    }
    public void ReturnGameObjectToPool(GameObject obj)
    {
        pool.Release(obj);
    }
}
