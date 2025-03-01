using UnityEngine;

public enum AnimalState
{
    None,
    Idle,
    Wander,
    //Flocking,
    //Fleeing,
    //Baited,
    WantsToFuck,
    //Sleeping
}

public class AnimalStateMachine : BaseStateMachine
{
    [SerializeField] private Counter _idleCounter;
    [SerializeField] private AnimalStats _stats;

    public AnimalState RandomAnimalState = AnimalState.None;
    public float RandomWanderRange => _stats.RandomWanderRange;
    public AnimalTypes AnimalType => _stats.AnimalType;

    protected override void Awake()
    {
        base.Awake();

        _idleCounter = new Counter(_stats.IdleTimeRange.Min);
        _idleCounter.CheatCurrentTick(_stats.IdleTimeRange.Min + 1);
    }

    public void ResetCounter() => _idleCounter.ResetWithRandomness(_stats.IdleTimeRange.Min, _stats.IdleTimeRange.Max);
    public void IncrementCounter() => _idleCounter.Increment();
    public bool IsCounterDone() => _idleCounter.IsDone();
}
