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

    public UnlockInterface unlockInterface;
    public SummaryInterface summaryInterface;

    private void OnEnable()
    {
        GameEvent.Register(Event.DayStart, OnDayStart);
        GameEvent.Register(Event.NightStart, OnNightStart);
        GameEvent.Register(Event.MoneyChanged, OnMoneyChanged);
        GameEvent<bool>.Register(Event.DisplayDaySummary, DisplayDaySummary);
        GameEvent<DayEventBase>.Register(Event.DisplayDayEvent, DisplayDayUnlock);
        GameEvent <float>.Register(Event.MoneyPreviewReceived, OnMoneyPreviewReceived);
    }

    private void OnDisable()
    {
        GameEvent.Unregister(Event.DayStart, OnDayStart);
        GameEvent.Unregister(Event.NightStart, OnNightStart);
        GameEvent.Unregister(Event.MoneyChanged, OnMoneyChanged);
        GameEvent<bool>.Unregister(Event.DisplayDaySummary, DisplayDaySummary);
        GameEvent<DayEventBase>.Unregister(Event.DisplayDayEvent, DisplayDayUnlock);
        GameEvent<float>.Unregister(Event.MoneyPreviewReceived, OnMoneyPreviewReceived);
    }

    private void Start()
    {
        GetComponent<MoneyMenu>().displayedImage.sprite = moneySprite;
        OnMoneyChanged();
    }

    private void SelectPower(GameObject button)
    {
        ConstructionManager.Instance.ChangeSelectedConstruction(button.GetComponent<LevelMenuButton>().index);
    }

    private void OnDayStart()
    {
        foreach (GameObject button in actionButtonList)
        {
            Destroy(button);
        }

        actionButtonList.Clear();
        unlockInterface.gameObject.SetActive(false);
    }

    private void DisplayDaySummary(bool active)
    {
        summaryInterface.gameObject.SetActive(active);
    }

    private void DisplayDayUnlock(DayEventBase dayEvent)
    { 
        if (dayEvent != null)
        {
            unlockInterface.gameObject.SetActive(true);
            unlockInterface.Display(dayEvent);
        }
        else
        {
            unlockInterface.gameObject.SetActive(false);
        }
    }

    private void OnNightStart()
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