using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimalManager : Singleton<AnimalManager>
{
    public List<AnimalPrefabs> Prefabs;

    [SerializeField] private List<GameObject> _animals = new();
    private IDictionary<AnimalTypes, GameObject> _prefabs = new SortedDictionary<AnimalTypes, GameObject>();

    private int _dayDeath = 0;
    public float SpawnOffset = 0.5f;


    private void Start()
    {
        _prefabs = Prefabs.ToDictionary(key => key.Type, value => value.Prefab);
    }

    private void OnEnable()
    {
        GameEvent<AnimalTypes, Vector3>.Register(Event.SpawnAnimal, OnSpawnAnimal);
        GameEvent<GameObject>.Register(Event.KillAnimal, OnKillAnimal);
        GameEvent.Register(Event.DayStart, OnDayStart);
    }

    private void OnDisable()
    {
        GameEvent<AnimalTypes, Vector3>.Unregister(Event.SpawnAnimal, OnSpawnAnimal);
        GameEvent<GameObject>.Unregister(Event.KillAnimal, OnKillAnimal);
        GameEvent.Unregister(Event.DayStart, OnDayStart);
    }

    protected virtual void OnSpawnAnimal(AnimalTypes type, Vector3 spawnPosition)
    {
        spawnPosition.x += SpawnOffset;

        GameObject newAnimal = Instantiate(_prefabs[type], spawnPosition, Quaternion.identity);
        var asm = newAnimal.GetComponent<AnimalStateMachine>();
        asm.DayBorn = GameManager.Instance.CurrentDay;
        asm.CurrentBabyBorn = asm.MaxBabyPerDay;
        _animals.Add(newAnimal);
    }

    protected virtual void OnKillAnimal(GameObject animal)
    {
        _dayDeath++;
    }

    protected virtual void OnDayStart()
    {
        _dayDeath = 0;
    }

    public int GetAnimalCount() => _animals.Count;
    public int GetAnimalCount(AnimalTypes type) => _animals
        .Count(x => x.TryGetComponent<AnimalStateMachine>(out var asm) && asm.AnimalType == type);
    public int GetAnimalCount(int dayBorn) =>
        _animals.Count(x => x.TryGetComponent<AnimalStateMachine>(out var asm) && asm.DayBorn == dayBorn);
    public int GetAnimalDeathCount() => _dayDeath;
}

[Serializable]
public class AnimalPrefabs
{
    [SerializeField] public AnimalTypes Type;
    [SerializeField] public GameObject Prefab;
}