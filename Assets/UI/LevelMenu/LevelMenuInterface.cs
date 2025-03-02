using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenuInterface : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject buttonParent;
    public GameObject ProductInfoElementPrefab;
    public GameObject ContinueButtonPrefab;

    public Sprite moneySprite;
    private List<GameObject> actionButtonList = new();
    private List<GameObject> _productInfos = new();

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
        if (active)
            CreateInfos();
        else
            DestroyInfos();

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

    public void CreateInfos()
    {
        foreach (var animalType in AnimalManager.Instance.GetAnimalTypesForTest())
        {
            GameObject alive = Instantiate(ProductInfoElementPrefab,
                summaryInterface.parentContainer.transform);
            var alivePanel = alive.GetComponent<SummaryPanel>();
            //alivePanel.Image = "SOMETHING";
            alivePanel.Name.text = $"{animalType.ProductName}(s) sold for:";
            alivePanel.Quantity.text = AnimalManager.Instance.TotalValue(animalType.AnimalType).ToString();
            _productInfos.Add(alive);

            GameObject dead = Instantiate(ProductInfoElementPrefab,
                summaryInterface.parentContainer.transform);
            var deadPanel = dead.GetComponent<SummaryPanel>();
            //dead.Image = "SOMETHING";
            deadPanel.Name.text = $"{Enum.GetName(typeof(AnimalTypes), animalType.AnimalType)} dead:";
            deadPanel.Quantity.text = AnimalManager.Instance.GetAnimalCount(animalType.AnimalType).ToString();
            _productInfos.Add(dead);
        }

        GameObject totalValue = Instantiate(ProductInfoElementPrefab,
            summaryInterface.parentContainer.transform);
        var totalValuePanel = totalValue.GetComponent<SummaryPanel>();
        //dead.Image = "SOMETHING";
        totalValuePanel.Name.text = $"Total: ";
        totalValuePanel.Quantity.text = AnimalManager.Instance.TotalValue().ToString();
        _productInfos.Add(totalValue);

        GameObject button = Instantiate(ContinueButtonPrefab,
            summaryInterface.parentContainer.transform);
        _productInfos.Add(button);
    }

    public void DestroyInfos()
    {
        foreach (var info in _productInfos)
        {
            _productInfos.Remove(info);
            Destroy(info);
        }
    }
}