using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public float Money;
    public float LenghtOfDay;
    public float LenghtOfNight;

    public bool StMethodeIsASecteurOfStFelicien = false;

    public int CurrentDay = 1;

    private IEnumerator _couroutine;

    public AudioClip MenuMusic;
    public AudioClip DayMusic;
    public AudioClip NightMusic;

    public List<DayEventBase> dayEvents = new List<DayEventBase>(); 

    public void StartOfDay()
    {
        Debug.Log("StartOfDay");
        bool waitingForUnlockConfirmation = false;

        foreach (DayEventBase dayEvent in dayEvents)
        {
            if (dayEvent.day == CurrentDay)
            {
                waitingForUnlockConfirmation = true;
                dayEvent.OnUnlock();
                GameEvent<DayEventBase>.Call(Event.DisplayDayEvent, dayEvent);
            }
        }

        if (!waitingForUnlockConfirmation)
        {
            OnStartOfDayCallback();
        }
    }

    public void OnStartOfDayCallback()
    {
        Debug.Log("OnStartOfDayCallback");
        GameEvent<DayEventBase>.Call(Event.DisplayDayEvent, null);
        _couroutine = DayCycleCountDown(LenghtOfDay);
        StartCoroutine(_couroutine);
    }

    public void EndOfDay()
    {
        Debug.Log("EndOfDay");
        StartOfNight();
    }

    public void StartOfNight()
    {
        Debug.Log("StartOfNight");
        _couroutine = NightCycleCountDown(LenghtOfNight);
        StartCoroutine(_couroutine);
    }

    public void EndOfNight()
    {
        Debug.Log("EndOfNight");
        // if AnimalManager.Instance.animalCount == 0 -> GameOver();
        // else displayDaySummary();

        GameEvent<bool>.Call(Event.DisplayDaySummary, true);
    }

    public void OnEndOfNightCallback()
    {
        GameEvent<bool>.Call(Event.DisplayDaySummary, false);
        CurrentDay++;
        StartOfDay();
    }

    public void GameOver()
    {
        /*Debug.Log("Jay, put the shader here!");*/
    }

    public void PayMoney(float amount)
    {
        Money -= amount;
        GameEvent.Call(Event.MoneyChanged);
    }

    private IEnumerator DayCycleCountDown(float wait)
    {
        GameEvent.Call(Event.DayStart);
        yield return new WaitForSeconds(wait);
        GameEvent.Call(Event.DayEnd);
        EndOfDay();
    }

    private IEnumerator NightCycleCountDown(float wait)
    {
        GameEvent.Call(Event.NightStart);
        yield return new WaitForSeconds(wait);
        GameEvent.Call(Event.NightEnd);
        EndOfNight();
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartOfDay();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
