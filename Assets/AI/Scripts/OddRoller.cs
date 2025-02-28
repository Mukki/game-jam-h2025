public class OddRoller
{
    private readonly int _maxOdds;
    private int _currentOdds;
    private bool _isInCooldown = false;

    public OddRoller(int maxExcludedOdds)
    {
        _maxOdds = maxExcludedOdds;
        _currentOdds = maxExcludedOdds;
    }

    public void ResetOdds()
    {
        _currentOdds = _maxOdds;
    }

    public void IncreaseOdds()
    {
        _currentOdds--;
    }

    public int Roll(bool increaseOdds = false)
    {
        var roll = UnityEngine.Random.Range(0, _currentOdds);

        if (increaseOdds)
            IncreaseOdds();

        return roll;
    }

    public int RollWithCooldown(bool increaseOdds = false)
    {
        if (_isInCooldown)
            return _currentOdds;

        _isInCooldown = true;
        return Roll(increaseOdds);
    }

    public void ResetCooldown()
    {
        _isInCooldown = false;
    }
}