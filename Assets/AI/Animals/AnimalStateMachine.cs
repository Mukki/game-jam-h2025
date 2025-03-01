using UnityEngine;

public enum AnimalState
{
    None,
    Idle,
    Wander,
    //Flocking,
    //Fleeing,
    //Baited,
    //WantsToFuck,
    //Sleeping
}

public class AnimalStateMachine : BaseStateMachine
{
    [SerializeField] private Counter _idleCounter;
    [SerializeField] private BoundedValues _counterTime = new() 
    {
        Min = 100,
        Max = 150
    };

    public AnimalState RandomAnimalState = AnimalState.None;
    public float RandomWanderRange = 1.5f;

    protected override void Awake()
    {
        base.Awake();

        _idleCounter = new Counter(_counterTime.Min);
        _idleCounter.CheatCurrentTick(_counterTime.Min + 1);
    }

    public void ResetCounter() => _idleCounter.ResetWithRandomness(_counterTime.Min, _counterTime.Max);
    public void IncrementCounter() => _idleCounter.Increment();
    public bool IsCounterDone() => _idleCounter.IsDone();
}
