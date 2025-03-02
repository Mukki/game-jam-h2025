public class Counter
{
    private int MaxTick;
    private int CurrentTick;

    // Randomized Counter
    public Counter(int minTick, int maxTick)
    {
        MaxTick = UnityEngine.Random.Range(minTick, maxTick);
        CurrentTick = 0;
    }

    public Counter(int maxTick)
    {
        MaxTick = maxTick;
        CurrentTick = 0;
    }

    public void Increment()
    {
        if (CurrentTick < MaxTick)
            CurrentTick++;
    }

    public void Reset() => CurrentTick = 0;
    public bool IsDone() => CurrentTick >= MaxTick;
    public void CheatCurrentTick(int value) => CurrentTick = value;

    public void ResetWithRandomness(int minCounterTick, int maxCounterTick)
    {
        MaxTick = UnityEngine.Random.Range(minCounterTick, maxCounterTick);
        CurrentTick = 0;
    }

    public int GetCurrentTick() => CurrentTick;

}