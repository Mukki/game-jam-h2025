using System.Collections;
using System.Collections.Generic;
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

    public List<Transform> animalSpawns = new List<Transform>();
    public List<GameObject> animals = new List<GameObject>();
    public List<GameObject> animalPrefabs = new List<GameObject>();
    public ShadowHandController shadowHandController;

    private void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
        StartCoroutine(VictimizeAnimals());
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

    private IEnumerator VictimizeAnimals()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3, 8));

            int index = Random.Range(0, animals.Count);
            GameObject victim = animals[index];
            animals[index] = null;
            shadowHandController.target = victim.transform;

            yield return new WaitForSeconds(Random.Range(10, 15));

            animals[index] = Instantiate(animalPrefabs[Random.Range(0, animalPrefabs.Count)], animalSpawns[index].position, animalSpawns[index].rotation);
        }
    }
}
