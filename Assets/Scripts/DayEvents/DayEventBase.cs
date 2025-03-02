using UnityEngine;

public abstract class DayEventBase : ScriptableObject
{
    public int day;
    public string description;
    public Sprite sprite;

    public abstract void OnUnlock();
}
