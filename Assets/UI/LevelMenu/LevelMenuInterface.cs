using UnityEngine;
using UnityEngine.UI;

public class LevelMenuInterface : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject buttonParent;

    public Sprite moneySprite;

    private List<GameObject> actionButtonList;

    private void OnEnable()
    {
        GameEvent.Register(Event.EnableActionButtons, ShowButtons);
        GameEvent.Register(Event.DisableActionButtons, HideButtons);
    }

    private void Start()
    {
        int currentIndex = 0;
        foreach (ConstructionBase construction in ConstructionManager.Instance.availableConstructions)
        {
            GameObject newButton = Instantiate(buttonPrefab, buttonParent.transform);
            actionButtonList.Add(newButton);
            newButton.GetComponent<LevelMenuButton>().index = currentIndex;
            newButton.GetComponent<LevelMenuButton>().textButton.text = construction.Name;
            newButton.GetComponent<LevelMenuButton>().displayedImage.sprite = construction.Image; 
            newButton.GetComponent<Button>().onClick.AddListener(() => SelectPower(newButton));
            currentIndex++;
        }

        GetComponent<MoneyMenu>().displayedImage.sprite = moneySprite;
        GetComponent<MoneyMenu>().currentMoney.text = " X " + GameManager.Instance.money;
    }

    private void SelectPower(GameObject button)
    {
        ConstructionManager.Instance.ChangeSelectedConstruction(button.GetComponent<LevelMenuButton>().index);
    }

    private void HideButtons()
    {
        foreach (GameObject button in actionButtonList)
        {
            button.SetActive(false);
        }
    }

    private void ShowButtons()
    {
        foreach (GameObject button in actionButtonList)
        {
            button.SetActive(true);
        }
    }
}