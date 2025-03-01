using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public float money;
    public float lenghOfDay;
    public float lenghOfNight;

    public bool stMethodeIsASecteurOfStFelicien = true;

    public override void Awake()
    {
        base.Awake();

        money = 100.0f;
    }

    public void OnEnable()
    {
        GameEvent.Register(Event.DayStart, StartOfDay);
        GameEvent.Register(Event.DayEnd, EndOfDay);
        GameEvent.Register(Event.NightStart, StartOfNight);
        GameEvent.Register(Event.NightEnd, EndOfNight);
        GameEvent.Register(Event.GameEnd, GameOver);
    }

    public void OnDisable()
    {
        GameEvent.Unregister(Event.DayStart, StartOfDay);
        GameEvent.Unregister(Event.DayEnd, EndOfDay);
        GameEvent.Unregister(Event.NightStart, StartOfNight);
        GameEvent.Unregister(Event.NightEnd, EndOfNight);
        GameEvent.Unregister(Event.GameEnd, GameOver);
    }

    public void StartOfDay()
    {
        GameEvent.Call(Event.EnableActionButtons);
    }

    public void EndOfDay()
    {
        GameEvent.Call(Event.DisableActionButtons);

        GameEvent.Call(Event.NightStart);
    }

    public void StartOfNight()
    {
         Debug.Log("NightStart");
         Debug.Log("Start Night timer");
         Debug.Log("End Night timer");
         GameEvent.Call(Event.NightEnd);
    }

    public void EndOfNight()
    {
        Debug.Log("if AnimalManager.Instance.animalCount == 0 -> GameOver()");
        Debug.Log("else GameEvent.Call(Event.EnableActionButtons)");
    }

    public void GameOver()
    {
        Debug.Log("Jay, put the shader here!");
    }

}
