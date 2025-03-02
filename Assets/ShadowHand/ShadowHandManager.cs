using System.Collections.Generic;
using UnityEngine;

public class ShadowHandManager : Singleton<ShadowHandManager>
{
    public int handCount = 0;
    public GameObject handPrefab;
    private List<GameObject> hands = new List<GameObject>();

    private void OnEnable()
    {
        GameEvent.Register(Event.DayStart, OnDayStart);
        GameEvent.Register(Event.NightStart, OnNightStart);
    }

    private void OnDisable()
    {
        GameEvent.Unregister(Event.DayStart, OnDayStart);
        GameEvent.Unregister(Event.NightStart, OnNightStart);
    }

    private void OnDayStart()
    {
        foreach (GameObject hand in hands)
        {
            Destroy(hand);
        }

        hands.Clear();
    }

    private void OnNightStart()
    {
        for (int i = 0; i < handCount; i++)
        {
            hands.Add(Instantiate(handPrefab));
        }
    }
}
