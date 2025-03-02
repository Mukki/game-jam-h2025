using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

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

    [SerializeField] public Guid ID = Guid.NewGuid();

    public AnimalState RandomAnimalState = AnimalState.None;
    public float RandomWanderRange => _stats.RandomWanderRange;
    public float StoppingDistanceOffset => _stats.StoppingDistanceOffset;
    public AnimalTypes AnimalType => _stats.AnimalType;
    public FieldOfView HormoneRange => _stats.HormoneRange;
    public FieldOfView SmellRange => _stats.SmellRange;
    public int MaxBabyPerDay => _stats.MaxBabyPerDay;

    public GameObject FoodTarget;

    public bool ForceToSleep = false;

    public int DayBorn = 1;
    public int CurrentBabyBorn = 0;

    protected override void Awake()
    {
        base.Awake();

        _idleCounter = new Counter(_stats.IdleTimeRange.Min);
        _idleCounter.CheatCurrentTick(_stats.IdleTimeRange.Min + 1);

        _fuckSearchCounter = new Counter(_stats.HornyTimeRange.Min);
        CurrentBabyBorn = _stats.MaxBabyPerDay;

        var navAgent = GetComponent<NavMeshAgent>();
        navAgent.velocity = Vector3.zero;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        GameEvent.Register(Event.NightStart, ForceSleep);
        GameEvent.Register(Event.DayStart, ForceAwake);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        GameEvent.Unregister(Event.NightStart, ForceSleep);
        GameEvent.Unregister(Event.DayStart, ForceAwake);
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

    private bool _canFuck = true;
    private IEnumerator _coroutine;

    public bool IsFuckable(AnimalTypes partnerType)
    {
        return partnerType == _stats.AnimalType
            && !BreedingManager.Instance.IsInAnyCouple(ID)
            && ThisCanFuck();
    }

    public bool ThisCanFuck()
    {
        return CurrentBabyBorn < _stats.MaxBabyPerDay
            && _canFuck;
    }

    public void BirthedChild()
    {
        CurrentBabyBorn++;
        _canFuck = false;
        _coroutine = BreedCooldown(_stats.BreedingCooldown);
        StartCoroutine(_coroutine);
    }

    private IEnumerator BreedCooldown(float wait)
    {
        yield return new WaitForSeconds(wait);
        _canFuck = true;
    }
    /********/
}
