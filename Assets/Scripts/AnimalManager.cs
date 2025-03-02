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
        GameEvent.Register(Event.DayStart, OnDayStart);
        GameEvent<AnimalTypes, Vector3>.Register(Event.SpawnAnimal, OnSpawnAnimal);
        GameEvent<GameObject>.Register(Event.KillAnimal, OnKillAnimal);
    }

    private void OnDisable()
    {
        GameEvent.Unregister(Event.DayStart, OnDayStart);
        GameEvent<AnimalTypes, Vector3>.Unregister(Event.SpawnAnimal, OnSpawnAnimal);
        GameEvent<GameObject>.Unregister(Event.KillAnimal, OnKillAnimal);
    }

    protected virtual void OnSpawnAnimal(AnimalTypes type, Vector3 spawnPosition)
    {
        spawnPosition.x += SpawnOffset;
        SpawnAnimal(type, spawnPosition);
    }

    public void SpawnAnimalBasedOnTerrain(AnimalTypes type, GameObject terrain)
    {
        if (terrain.TryGetComponent<Collider>(out var collider))
        {
            var minPos = collider.bounds.min;
            var maxPos = collider.bounds.max;

            var targetPos = new Vector3(
                UnityEngine.Random.Range(minPos.x, maxPos.x), 
                0.5f, 
                UnityEngine.Random.Range(minPos.z, maxPos.z)
            );

            SpawnAnimal(type, targetPos);
        }
    }

    private void SpawnAnimal(AnimalTypes type, Vector3 spawnPosition)
    {
        GameObject newAnimal = Instantiate(_prefabs[type], spawnPosition, Quaternion.identity);
        var asm = newAnimal.GetComponent<AnimalStateMachine>();
        asm.DayBorn = GameManager.Instance.CurrentDay;
        _animals.Add(newAnimal);
    }

    protected virtual void OnKillAnimal(GameObject animal)
    {
        _dayDeath++;

        if (animal.TryGetComponent<AnimalStateMachine>(out var asm))
        {
            var toRemove = _animals.FirstOrDefault(x => x.TryGetComponent<AnimalStateMachine>(out var xasm) 
                && xasm.ID == asm.ID);

            BreedingManager.Instance.DestroyCouple(asm);

            if (toRemove != null)
            {
                _animals.Remove(toRemove);
            }
        }

        Destroy(animal);
    }

    public GameObject GetRandomAnimal()
    {
        return _animals[UnityEngine.Random.Range(0, _animals.Count)];
    }

    protected virtual void OnDayStart()
    {
        _dayDeath = 0;
        BreedingManager.Instance.ClearCouples();
    }

    public int GetAnimalCount() => _animals.Count;
    public int GetAnimalCount(AnimalTypes type) => _animals
        .Count(x => x.TryGetComponent<AnimalStateMachine>(out var asm) && asm.AnimalType == type);
    public int GetAnimalCount(int dayBorn) =>
        _animals.Count(x => x.TryGetComponent<AnimalStateMachine>(out var asm) && asm.DayBorn == dayBorn);

    public int GetAnimalDeathCount() => _dayDeath;

    public int TotalValue() => _animals.Sum(x => x.GetComponent<AnimalStateMachine>().SellValue);
    public int TotalValue(AnimalTypes type) => _animals
        .Where(x => x.TryGetComponent<AnimalStateMachine>(out var asm) && asm.AnimalType == type)
        .Sum(x => x.GetComponent<AnimalStateMachine>().SellValue);

    public List<AnimalInfo> GetAnimalTypes() => 
        _animals
        .Select(x => new AnimalInfo()
            {
                AnimalType = x.GetComponent<AnimalStateMachine>().AnimalType,
                ProductName = x.GetComponent<AnimalStateMachine>().ProductName,
        })
        .Distinct()
        .ToList();

    public List<AnimalInfo> GetAnimalTypesForTest() => new List<AnimalInfo>()
    {
        new AnimalInfo()
        {
            AnimalType = AnimalTypes.Sheep,
            ProductName = "Wool"
        },
        new AnimalInfo()
        {
            AnimalType = AnimalTypes.Cow,
            ProductName = "Milk"
        },
    };
}

[Serializable]
public class AnimalPrefabs
{
    [SerializeField] public AnimalTypes Type;
    [SerializeField] public GameObject Prefab;
}

public class AnimalInfo
{
    public AnimalTypes AnimalType;
    public string ProductName;
}