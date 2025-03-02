using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuController : MonoBehaviour
{
    public VisualElement ui;

    public Button playButton;
    public Button rulesButton;
    public Button quitButton;

    private void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
    }

    private void OnEnable()
    {
        playButton = ui.Q<Button>("PlayButton");
        playButton.clicked += OnPlayButtonClicked;

        rulesButton = ui.Q<Button>("RulesButton");
        rulesButton.clicked += OnRulesButtonClicked;

        quitButton = ui.Q<Button>("QuitButton");
        quitButton.clicked += OnQuitButtonClicked;
    }

    private void OnDisable()
    {
        playButton.clicked -= OnPlayButtonClicked;

        rulesButton.clicked -= OnRulesButtonClicked;

        quitButton.clicked -= OnQuitButtonClicked;
    }

    private void OnPlayButtonClicked()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene("GameScene");
    }

    private void OnRulesButtonClicked()
    {
        Debug.Log("Rules opened!");
    }

    private void OnQuitButtonClicked()
    {
        Application.Quit();
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #endif
    }
}
