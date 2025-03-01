using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public float money;

    protected override void OnAwake()
    {
        base.OnAwake();

        money = 100.0f;
    }
}
