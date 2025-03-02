using System;
using System.Collections;
using UnityEngine;

public enum AnimalState
{
    None,
    Idle,
    Wander,
    //Flocking,
    //Fleeing,
    Baited,
    WantsToFuck,
    //Sleeping
}

public class AnimalStateMachine : BaseStateMachine
{
    [SerializeField] private Counter _idleCounter;
    [SerializeField] private Counter _fuckSearchCounter;
    [SerializeField] private AnimalStats _stats;

    public Guid id = Guid.NewGuid();

    public AnimalState RandomAnimalState = AnimalState.None;
    public float RandomWanderRange => _stats.RandomWanderRange;
    public float StoppingDistanceOffset => _stats.StoppingDistanceOffset;
    public AnimalTypes AnimalType => _stats.AnimalType;
    public FieldOfView HormoneRange => _stats.HormoneRange;
    public FieldOfView SmellRange => _stats.SmellRange;
    public int MaxBabyPerDay => _stats.MaxBabyPerDay;

    public GameObject FoodTarget;
    public GameObject FuckTarget;
    public bool WillSpawnBaby = false;
    public bool ForceToFuck = false;

    public bool ForceToSleep = false;

    public int DayBorn = 1;
    public int CurrentBabyBorn = 0;

    protected override void Awake()
    {
        base.Awake();

        _idleCounter = new Counter(_stats.IdleTimeRange.Min);
        _idleCounter.CheatCurrentTick(_stats.IdleTimeRange.Min + 1);

        _fuckSearchCounter = new Counter(_stats.HornyTimeRange.Min);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        GameEvent.Register(Event.NightStart, ForceSleep);
        GameEvent.Register(Event.NightEnd, ForceAwake);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        GameEvent.Unregister(Event.NightStart, ForceSleep);
        GameEvent.Unregister(Event.NightEnd, ForceAwake);
    }

    private void ForceSleep()
    {
        ForceToSleep = true;
    }

    private void ForceAwake()
    {
        ForceToSleep = false;
        CurrentBabyBorn = 0;
    }

    /* Idle */
    public void ResetIdleCounter() => _idleCounter.ResetWithRandomness(_stats.IdleTimeRange.Min, _stats.IdleTimeRange.Max);
    public void IncrementIdleCounter() => _idleCounter.Increment();
    public bool IsIdleCounterDone() => _idleCounter.IsDone();
    /********/

    /* Fuck */
    public void ResetSearchFuckCounter() => _fuckSearchCounter.ResetWithRandomness(_stats.HornyTimeRange.Min, _stats.HornyTimeRange.Max);
    public void IncrementSearchFuckCounter() => _fuckSearchCounter.Increment();
    public bool IsSearchFuckCounterDone() => _fuckSearchCounter.IsDone();

    public bool IsFuckable(AnimalTypes partnerType)
    {
        return partnerType == _stats.AnimalType 
            && FuckTarget == null
            && CurrentBabyBorn < _stats.MaxBabyPerDay;
    }
    /********/
}
