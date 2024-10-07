using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer lineRenderer;

    public float offsetSpeed;
    void Update()
    {
        if (lineRenderer != null)
        {
            var offset = lineRenderer.material.mainTextureOffset;

            offset.x += offsetSpeed * Time.deltaTime;

            lineRenderer.material.mainTextureOffset=offset;
        }
    }
}
