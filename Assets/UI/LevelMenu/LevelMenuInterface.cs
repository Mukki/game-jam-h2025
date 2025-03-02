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
    public Sprite Money;

    public float ClockSpeed;
    public bool DayClockIsActive = false;
    public bool NightClockIsActive = false;

    public RectTransform Clock;

    public Counter DayTimer;
    public Counter NightTimer;

    public Sprite moneySprite;
    private List<GameObject> actionButtonList = new();
    private List<GameObject> _productInfos = new();

    public UnlockInterface unlockInterface;
    public SummaryInterface summaryInterface;

    private void Awake()
    {
        DayTimer = new Counter((int)GameManager.Instance.LenghtOfDay * 50);
        NightTimer = new Counter((int)GameManager.Instance.LenghtOfNight * 50);
    }

    private void OnEnable()
    {
        GameEvent.Register(Event.DayStart, OnDayStart);
        GameEvent.Register(Event.DayEnd, OnDayEnd);
        GameEvent.Register(Event.NightStart, OnNightStart);
        GameEvent.Register(Event.NightEnd, OnNightEnd);
        GameEvent.Register(Event.MoneyChanged, OnMoneyChanged);
        GameEvent<bool>.Register(Event.DisplayDaySummary, DisplayDaySummary);
        GameEvent<DayEventBase>.Register(Event.DisplayDayEvent, DisplayDayUnlock);
        GameEvent <float>.Register(Event.MoneyPreviewReceived, OnMoneyPreviewReceived);
    }

    private void OnDisable()
    {
        GameEvent.Unregister(Event.DayStart, OnDayStart);
        GameEvent.Unregister(Event.DayEnd, OnDayEnd);
        GameEvent.Unregister(Event.NightStart, OnNightStart);
        GameEvent.Unregister(Event.NightEnd, OnNightEnd);
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

        unlockInterface.gameObject.SetActive(false);
        
        DayClockIsActive = true;
    }

    private void OnDayEnd()
    {
        DayClockIsActive = false;
        NightTimer.Reset();
    }

    private void DisplayDaySummary(bool active)
    {
        summaryInterface.gameObject.SetActive(active);

        if (active)
            CreateInfos();
        else
            DestroyInfos();
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
        NightClockIsActive = true;
    }

    private void OnNightEnd()
    {
        GameManager.Instance.PayMoney(-AnimalManager.Instance.TotalValue());
        NightClockIsActive = false;
        DayTimer.Reset();
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
        foreach (var animalType in AnimalManager.Instance.GetAnimalInfos())
        {
            GameObject alive = Instantiate(ProductInfoElementPrefab,
                summaryInterface.parentContainer.transform);
            var alivePanel = alive.GetComponent<SummaryPanel>();
            alivePanel.Image.sprite = animalType.Photo;
            alivePanel.Name.text = $"{animalType.ProductName}(s) sold for:";
            alivePanel.Quantity.text = AnimalManager.Instance.TotalValue(animalType.AnimalType).ToString();
            _productInfos.Add(alive);

            /*
            GameObject dead = Instantiate(ProductInfoElementPrefab,
                summaryInterface.parentContainer.transform);
            var deadPanel = dead.GetComponent<SummaryPanel>();
            deadPanel.Name.text = $"{Enum.GetName(typeof(AnimalTypes), animalType.AnimalType)} dead:";
            deadPanel.Quantity.text = AnimalManager.Instance.GetAnimalCount(animalType.AnimalType).ToString();
            _productInfos.Add(dead);
            */
        }

        GameObject totalValue = Instantiate(ProductInfoElementPrefab,
            summaryInterface.parentContainer.transform);
        var totalValuePanel = totalValue.GetComponent<SummaryPanel>();
        totalValuePanel.Name.text = $"Total: ";
        totalValuePanel.Quantity.text = AnimalManager.Instance.TotalValue().ToString();

        if (Money != null)
        {
            totalValuePanel.Image.sprite = Money;
        }
        _productInfos.Add(totalValue);

        GameObject button = Instantiate(ContinueButtonPrefab,
            summaryInterface.parentContainer.transform);
        button.GetComponent<Button>().onClick.AddListener(() => GameManager.Instance.OnEndOfNightCallback());
        _productInfos.Add(button);
    }

    public void DestroyInfos()
    {
        if (_productInfos != null)
        {
            foreach (var info in _productInfos)
            {
                Destroy(info);
            }
        }
    }

    private void FixedUpdate()
    {
        if (DayClockIsActive)
        {
            DayTimer.Increment();
            int current = DayTimer.GetCurrentTick();
            Clock.rotation = Quaternion.Euler(0, 0, current/50 * 180/GameManager.Instance.LenghtOfDay);
        }

        if (NightClockIsActive)
        {
            NightTimer.Increment();
            int current = NightTimer.GetCurrentTick();
            Clock.rotation = Quaternion.Euler(0, 0, (current/50 * 180/GameManager.Instance.LenghtOfNight) + 180);
        }
    }
}