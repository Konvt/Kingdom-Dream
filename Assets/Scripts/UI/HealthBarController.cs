using UnityEngine;
using UnityEngine.UIElements;

public class HealthBarController : MonoBehaviour
{
    public Transform healthBarTransform;

    private UIDocument healthBarDocument;

    private ProgressBar healthBar;

    private void Awake()
    {
        healthBarDocument = GetComponent<UIDocument>();

        healthBar= healthBarDocument.rootVisualElement.Q<ProgressBar>("HealthBar");

        MoveToWorldPosition(healthBar, healthBarTransform.position,Vector2.zero);
    }

    private void MoveToWorldPosition(VisualElement element, Vector3 worldPosition, Vector2 size)
    {
        Rect rect = RuntimePanelUtils.CameraTransformWorldToPanelRect(element.panel, worldPosition, size,Camera.main);
        element.transform.position = rect.position;
        
    }
}
