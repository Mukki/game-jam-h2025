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

    private IEnumerator _coroutine;

    public AudioClip MenuMusic;
    public AudioClip DayMusic;
    public AudioClip NightMusic;

    public List<DayEventBase> dayEvents = new List<DayEventBase>(); 

    public void StartOfDay()
    {
        Debug.Log("StartOfDay");
        SoundManager.Instance.MySource.clip = SoundManager.Instance.DayAudio;
        SoundManager.Instance.MySource.Play();
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
        _coroutine = DayCycleCountDown(LenghtOfDay);
        StartCoroutine(_coroutine);
    }

    public void EndOfDay()
    {
        Debug.Log("EndOfDay");
        StartOfNight();
    }

    public void StartOfNight()
    {
        Debug.Log("StartOfNight");
        SoundManager.Instance.MySource.clip = SoundManager.Instance.NightAudio;
        SoundManager.Instance.MySource.Play();
        _coroutine = NightCycleCountDown(LenghtOfNight);
        StartCoroutine(_coroutine);
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
        SoundManager.Instance.MySource.Stop();
        EndOfDay();
    }

    private IEnumerator NightCycleCountDown(float wait)
    {
        GameEvent.Call(Event.NightStart);
        yield return new WaitForSeconds(wait);
        GameEvent.Call(Event.NightEnd);
        SoundManager.Instance.MySource.Stop();
        EndOfNight();
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartOfDay();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
