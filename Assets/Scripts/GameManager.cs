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
        bool waitingForUnlockConfirmation = false;

        foreach (DayEventBase dayEvent in dayEvents)
        {
            if (dayEvent.day == CurrentDay)
            {
                dayEvent.OnUnlock();

                if (dayEvent.notifyPlayer)
                {
                    waitingForUnlockConfirmation = true;
                    SoundManager.Instance.MySource.clip = SoundManager.Instance.UnlockAudio;
                    SoundManager.Instance.MySource.loop = false;
                    SoundManager.Instance.MySource.Play();
                    GameEvent<DayEventBase>.Call(Event.DisplayDayEvent, dayEvent);
                }
            }
        }

        if (!waitingForUnlockConfirmation)
        {
            SoundManager.Instance.MySource.clip = SoundManager.Instance.MorningQueue;
            SoundManager.Instance.MySource.loop = false;
            SoundManager.Instance.MySource.Play();
            _coroutine = MorningQueue(3.0f);
            StartCoroutine(_coroutine);
        }
    }

    public void OnStartOfDayCallback()
    {
        Debug.Log("OnStartOfDayCallback");
        SoundManager.Instance.MySource.clip = SoundManager.Instance.DayAudio;
        SoundManager.Instance.MySource.loop = true;
        SoundManager.Instance.MySource.Play();
        DimableLight lightController = FindFirstObjectByType<DimableLight>();
        lightController.SetTargetIntensity(0.5f);
        ShadowBorderDim borderScript = FindFirstObjectByType<ShadowBorderDim>();
        borderScript.SetBorderValue(0.0f, 0.0f);
        GameEvent<DayEventBase>.Call(Event.DisplayDayEvent, null);
        _coroutine = DayCycleCountDown(LenghtOfDay);
        StartCoroutine(_coroutine);
    }

    public void EndOfDay()
    {
        Debug.Log("EndOfDay");
        SoundManager.Instance.MySource.clip = SoundManager.Instance.NightQueue;
        SoundManager.Instance.MySource.loop = false;
        SoundManager.Instance.MySource.Play();
        _coroutine = NightQueue(6.0f);
        StartCoroutine(_coroutine);
    }

    public void StartOfNight()
    {
        Debug.Log("StartOfNight");
        SoundManager.Instance.MySource.clip = SoundManager.Instance.NightAudio;
        SoundManager.Instance.MySource.loop = true;
        SoundManager.Instance.MySource.Play();
        DimableLight lightController = FindFirstObjectByType<DimableLight>();
        lightController.SetTargetIntensity(0.0f);
        ShadowBorderDim borderScript = FindFirstObjectByType<ShadowBorderDim>();
        borderScript.SetBorderValue(0.03f, 0.03f);
        _coroutine = NightCycleCountDown(LenghtOfNight);
        StartCoroutine(_coroutine);
    }

    public void EndOfNight()
    {
        Debug.Log("EndOfNight");
        // if AnimalManager.Instance.animalCount == 0 -> GameOver();
        // else displayDaySummary();
        Debug.Log("SummaryResults");
        SoundManager.Instance.MySource.clip = SoundManager.Instance.SumaryResult;
        SoundManager.Instance.MySource.loop = false;
        SoundManager.Instance.MySource.Play();
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
        ShadowBorderDim borderScript = FindFirstObjectByType<ShadowBorderDim>();
        borderScript.SetBorderValue(0.5f, 0.05f);
        SoundManager.Instance.MySource.clip = SoundManager.Instance.Death;
        SoundManager.Instance.MySource.loop = false;
        SoundManager.Instance.MySource.Play();
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

    private IEnumerator MorningQueue(float wait)
    {
        yield return new WaitForSeconds(wait);
        OnStartOfDayCallback();
    }

    private IEnumerator NightQueue(float wait)
    {
        yield return new WaitForSeconds(wait);
        StartOfNight();
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartOfDay();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
