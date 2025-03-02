using System;
using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    public float Money;
    public float LenghtOfDay;
    public float LenghtOfNight;

    public bool StMethodeIsASecteurOfStFelicien = true;

    public int CurrentDay = 1;

    private IEnumerator _couroutine;

    protected override void OnAwake()
    {
        base.OnAwake();

        Money = 100.0f;
        LenghtOfDay = 20.0f;
        LenghtOfNight = 40.0f;
    }

    public void StartOfDay()
    {
        _couroutine = DayCycleCountDown(LenghtOfDay);
        StartCoroutine(_couroutine);
    }

    public void EndOfDay()
    {
        StartOfNight();
    }

    public void StartOfNight()
    {
         _couroutine = NightCycleCountDown(LenghtOfNight);
         StartCoroutine(_couroutine);
    }

    public void EndOfNight()
    {
        // if AnimalManager.Instance.animalCount == 0 -> GameOver();
        // else StartOfDay();
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
}
