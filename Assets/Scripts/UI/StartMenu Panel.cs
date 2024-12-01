using UnityEngine;
using UnityEngine.UIElements;

public class StartMenuPanel : MonoBehaviour
{
    private VisualElement rootElement;
    private Button newGameButton;
    private Button exitGameButton;

    [Header("ÊÂ¼þ¹ã²¥")]
    public ObjectEventSO newGameEvent;
    private void OnEnable()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;

        newGameButton = rootElement.Q<Button>("NewGameButton");
        exitGameButton = rootElement.Q<Button>("ExitButton");

        newGameButton.clicked += OnNewGameButtonclicked;
        exitGameButton.clicked += ExitGame;
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void OnNewGameButtonclicked()
    {
        newGameEvent.RiseEvent(null, this);
    }
}
