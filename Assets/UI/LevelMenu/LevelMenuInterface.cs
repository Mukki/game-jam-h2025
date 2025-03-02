using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenuInterface : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject buttonParent;

    public float ClockSpeed;
    public bool DayClockIsActive = false;
    public bool NightClockIsActive = false;

    public RectTransform Clock;

    public Counter DayTimer;
    public Counter NightTimer;

    public Sprite moneySprite;
    private List<GameObject> actionButtonList = new List<GameObject>();

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

        NightClockIsActive = true;
    }

    private void OnNightEnd()
    {
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