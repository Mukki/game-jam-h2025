using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public float money;

    public override void Awake()
    {
        base.Awake();

        money = 100.0f;
    }

    public void ChangeMoney(float amount)
    {
        money += amount;
        GameEvent.Call(Event.MoneyChanged);
    }
}
