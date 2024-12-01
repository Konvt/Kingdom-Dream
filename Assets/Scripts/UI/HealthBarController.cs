using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthBarController : MonoBehaviour
{
    public Transform healthBarTransform;

    private UIDocument healthBarDocument;

    private ProgressBar healthBar;

    private CharacterBase currentCharater;

    private VisualElement defenseElement;
    private Label defenseAmountLabel;

    private VisualElement strengthElement;
    private Label strengthRoundLabel;
    public Sprite buffStrength;
    public Sprite debuffStrength;

    private Enemy enemy;
    private VisualElement intendElement;
    private Label intendAmountLabel;
    private void Awake()
    {
        currentCharater = GetComponent<CharacterBase>();
        if (gameObject.tag == "Player") return;
        enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        InitializeHealthBar();

    }
    public void InitializeHealthBar()
    {
        healthBarDocument = GetComponent<UIDocument>();

        healthBar = healthBarDocument.rootVisualElement.Q<ProgressBar>("HealthBar");

        healthBar.highValue = currentCharater.MaxHp;

        defenseElement = healthBar.Q<VisualElement>("Defense");
        defenseAmountLabel = defenseElement.Q<Label>("DefenseAmount");

        strengthElement = healthBar.Q<VisualElement>("Strength");
        strengthRoundLabel = strengthElement.Q<Label>("StrengthRound");

        intendElement = healthBar.Q<VisualElement>("Intend");
        intendAmountLabel = intendElement.Q<Label>("IntendAmount");

        defenseElement.style.display = DisplayStyle.None;
        strengthElement.style.display = DisplayStyle.None;
        intendElement.style.display=DisplayStyle.None;

        MoveToWorldPosition(healthBar, healthBarTransform.position, Vector2.zero);
    }
    private void MoveToWorldPosition(VisualElement element, Vector3 worldPosition, Vector2 size)
    {
        Rect rect = RuntimePanelUtils.CameraTransformWorldToPanelRect(element.panel, worldPosition, size,Camera.main);
        element.transform.position = rect.position;
        
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (currentCharater.isDead == true)
        {
            healthBar.style.display = DisplayStyle.None;
            return;
        }
        if (healthBar != null)
        {
            //血条UI更新
            healthBar.title =$"{currentCharater.CurrentHp}/{currentCharater.MaxHp}";
            healthBar.value = currentCharater.CurrentHp;

            healthBar.RemoveFromClassList("lowHealth");
            healthBar.RemoveFromClassList("mediumHealth");
            healthBar.RemoveFromClassList("highHealth");

            var percent = (float)currentCharater.CurrentHp / (float)currentCharater.MaxHp;

            if (percent < 0.3f) healthBar.AddToClassList("lowHealth");
            else if (percent<0.6f) healthBar.AddToClassList("mediumHealth");
            else healthBar.AddToClassList("highHealth");


            //防御技能UI逻辑
            defenseElement.style.display = currentCharater.defense.currentValue > 0 ? DisplayStyle.Flex : DisplayStyle.None;
            defenseAmountLabel.text = currentCharater.defense.currentValue.ToString();


            //拳套技能UI逻辑
            strengthElement.style.display=currentCharater.strengthRound.currentValue>0? DisplayStyle.Flex : DisplayStyle.None;
            strengthRoundLabel.text =currentCharater.strengthRound.currentValue.ToString();
            strengthElement.style.backgroundImage = currentCharater.baseStrength>1 ?new StyleBackground(buffStrength) : new StyleBackground(debuffStrength);

        }
    }

    public void SetIntendElement()
    {
        intendElement.style.display = DisplayStyle.Flex;
        intendElement.style.backgroundImage = new StyleBackground(enemy.currentAction.sprite);

        var value = enemy.currentAction.effect.value;
        if (enemy.currentAction.effect.GetType() == typeof(DamageEffect))
        {
            value = (int)math.round(value * enemy.baseStrength);
        }
        intendAmountLabel.text = value.ToString();
    }
    public void HideIntendElement()
    {
        intendElement.style.display = DisplayStyle.None;
     
    }
}
