using UnityEngine;

public abstract class DayEventBase : ScriptableObject
{
    public bool notifyPlayer;
    public int day;
    public string description;
    public string title;
    public Sprite sprite;

    public abstract void OnUnlock();
}
