using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimalManager : Singleton<AnimalManager>
{
    public List<AnimalPrefabs> Prefabs;

    [SerializeField] private List<GameObject> _animals = new();
    private IDictionary<AnimalTypes, GameObject> _prefabs = new SortedDictionary<AnimalTypes, GameObject>();

    private void Start()
    {
        _prefabs = Prefabs.ToDictionary(key => key.Type, value => value.Prefab);
    }

    private void OnEnable()
    {
        GameEvent<AnimalTypes, Vector3>.Register(Event.SpawnAnimal, OnSpawnAnimal);
    }

    private void OnDisable()
    {
        GameEvent<AnimalTypes, Vector3>.Unregister(Event.SpawnAnimal, OnSpawnAnimal);
    }

    protected virtual void OnSpawnAnimal(AnimalTypes type, Vector3 spawnPosition)
    {
        GameObject newAnimal = Instantiate(_prefabs[type], spawnPosition, Quaternion.identity);
        _animals.Add(newAnimal);
    }

    public int GetAnimalCount() => _animals.Count;
    public int GetAnimalOfTypeCount(AnimalTypes type) => _animals
        .Count(x => x.TryGetComponent<AnimalStateMachine>(out var asm) && asm.AnimalType == type);
}

[Serializable]
public class AnimalPrefabs
{
    [SerializeField] public AnimalTypes Type;
    [SerializeField] public GameObject Prefab;
}