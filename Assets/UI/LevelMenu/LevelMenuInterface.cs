using UnityEngine;
using UnityEngine.UI;

public class LevelMenuInterface : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject buttonParent;

    private void OnEnable()
    {
        int currentIndex = 0
        foreach (ConstructionBase construction in ConstructionManager.Instance.availableConstructions)
        {
            GameObject newButton = Instantiate(buttonPrefab, buttonParent.transform);
            newButton.GetComponent<LevelMenuButton>().index = currentIndex;
            newButton.GetComponent<LevelMenuButton>().textButton.text = construction.Name;
            newButton.GetComponent<LevelMenuButton>().displayedImage.sprite = construction.Image; 
            newButton.GetComponent<Button>().onClick.AddListener(() => SelectPower(currentIndex));
            currentIndex++;
        }
    }

    private void SelectPower(int currentIndex)
    {
        ConstructionManager.currentConstruction = currentIndex;
    }
}