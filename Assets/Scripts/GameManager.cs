using System;
using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    public float money;
    public float lenghtOfDay;
    public float lenghtOfNight;

    public bool stMethodeIsASecteurOfStFelicien = true;

    private IEnumerator couroutine;

    protected override void OnAwake()
    {
        base.OnAwake();

        money = 100.0f;
        lenghtOfDay = 20.0f;
        lenghtOfNight = 40.0f;
    }

    public void StartOfDay()
    {
        couroutine = DayCycleCountDown(lenghtOfDay);
        StartCoroutine(couroutine);
    }

    public void EndOfDay()
    {
        StartOfNight();
    }

    public void StartOfNight()
    {
         couroutine = NightCycleCountDown(lenghtOfNight);
         StartCoroutine(couroutine);
    }

    public void EndOfNight()
    {
        // if AnimalManager.Instance.animalCount == 0 -> GameOver();
        // else StartOfDay();
        StartOfDay();
    }

    public void GameOver()
    {
        /*Debug.Log("Jay, put the shader here!");*/
    }

    public void PayMoney(float amount)
    {
        money -= amount;
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
