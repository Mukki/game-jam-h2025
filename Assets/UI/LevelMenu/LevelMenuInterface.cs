using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenuInterface : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject buttonParent;

    public Sprite moneySprite;
    private List<GameObject> actionButtonList = new List<GameObject>();

    private void OnEnable()
    {
        GameEvent.Register(Event.DayStart, ShowButtons);
        GameEvent.Register(Event.NightStart, HideButtons);
        GameEvent.Register(Event.MoneyChanged, OnMoneyChanged);
        GameEvent <float>.Register(Event.MoneyPreviewReceived, OnMoneyPreviewReceived);
    }

    private void OnDisable()
    {
        GameEvent.Unregister(Event.DayStart, ShowButtons);
        GameEvent.Unregister(Event.NightStart, HideButtons);
        GameEvent.Unregister(Event.MoneyChanged, OnMoneyChanged);
        GameEvent<float>.Unregister(Event.MoneyPreviewReceived, OnMoneyPreviewReceived);
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
        OnMoneyChanged();
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

    public void OnMoneyChanged()
    {
        GetComponent<MoneyMenu>().currentMoney.text = " X " + GameManager.Instance.Money.ToString("0.00");
    }

    public void OnMoneyPreviewReceived(float amount)
    {
        GetComponent<MoneyMenu>().moneyPreview.gameObject.SetActive(amount > 0);
        GetComponent<MoneyMenu>().moneyPreview.text = "(-" + amount.ToString("0.00") + ")";
    }
}