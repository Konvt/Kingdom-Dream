using UnityEngine;

public class VFXController : MonoBehaviour
{
    public GameObject buffVFX;
    public GameObject debuffVFX;

    private float timeCounter;
    public float duration;

    private void Update()
    {
        if (buffVFX.activeInHierarchy)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= duration)
            {
                timeCounter = 0;
                buffVFX.SetActive(false);   
            }
        }
        if (debuffVFX.activeInHierarchy)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= duration)
            {
                timeCounter = 0;
                debuffVFX.SetActive(false);
            }
        }
    }

}
