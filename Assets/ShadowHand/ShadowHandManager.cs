using System.Collections.Generic;
using UnityEngine;

public class ShadowHandManager : Singleton<ShadowHandManager>
{
    public int handCount = 0;
    public GameObject handPrefab;
    private List<GameObject> hands = new List<GameObject>();

    private void OnEnable()
    {
        GameEvent.Register(Event.NightStart, OnNightStart);
        GameEvent.Register(Event.NightEnd, OnNightEnd);
    }

    private void OnDisable()
    {
        GameEvent.Unregister(Event.NightStart, OnNightStart);
        GameEvent.Unregister(Event.NightEnd, OnNightEnd);
    }

    private void OnNightEnd()
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
            Transform spawn = StuffSpawner.Instance.handSpawners[Random.Range(0, StuffSpawner.Instance.handSpawners.Count)];
            hands.Add(Instantiate(handPrefab, spawn.position, Quaternion.identity));
        }
    }
}
