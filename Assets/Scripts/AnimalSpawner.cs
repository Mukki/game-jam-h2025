using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : Singleton<AnimalSpawner>
{
    public int minAnimalCountPerTerrain;
    public int maxAnimalCountPerTerrain;
    public int initialTerrainCount;
    public List<GameObject> allTerrains = new List<GameObject>();
    public List<AnimalTypes> availableAnimals = new List<AnimalTypes>();

    private List<GameObject> spawnableTerrains = new List<GameObject>();

    protected override void OnAwake()
    {
        availableAnimals.Add(AnimalTypes.Cow);
        availableAnimals.Add(AnimalTypes.Chicken);

        AddSpawnableTerrains(initialTerrainCount);
    }

    private void OnEnable()
    {
        GameEvent.Register(Event.DayStart, OnDayStart);
    }

    private void OnDisable()
    {
        GameEvent.Unregister(Event.DayStart, OnDayStart);
    }

    private void OnDayStart()
    {
        foreach (GameObject terrain in spawnableTerrains)
        {
            for (int i = 0; i < Random.Range(minAnimalCountPerTerrain, maxAnimalCountPerTerrain); i++)
            {
                AnimalManager.Instance.SpawnAnimalBasedOnTerrain(availableAnimals[Random.Range(0, availableAnimals.Count)], terrain);
            }
        }

        spawnableTerrains.Clear();
    }

    public void AddSpawnableTerrains(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject terrain = allTerrains[Random.Range(0, allTerrains.Count)];
            spawnableTerrains.Add(terrain);
            allTerrains.Remove(terrain);
        }
    }
}
