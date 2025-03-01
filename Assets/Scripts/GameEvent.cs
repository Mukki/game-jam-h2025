using System;
using System.Collections.Generic;

public enum Event
{
    GameStarted,
    DayStart,
    DayEnd,
    NightStart,
    NightEnd,
    EnableActionButtons,
    DisableActionButtons,
    GameEnd,
}

// This implementation does not support params, to add params, see the exemple below
public class GameEvent : Singleton<GameEvent>
{
    readonly IDictionary<Event, Action> listeners = new SortedDictionary<Event, Action>();

    private GameEvent() { }

    public static void Register(Event eventId, Action callback) => Instance.RegisterImpl(eventId, callback);

    public static void Unregister(Event eventId, Action callback) => Instance.UnregisterImpl(eventId, callback);

    public static void UnregisterAll() => Instance.UnregisterAllImpl();

    public static void Call(Event eventId) => Instance.CallImpl(eventId);

    void RegisterImpl(Event eventId, Action callback)
    {
        if (!listeners.ContainsKey(eventId))
        {
            listeners.Add(eventId, callback);
        }
        else
        {
            listeners[eventId] += callback;
        }
    }

    void UnregisterImpl(Event eventId, Action callback)
    {
        if (listeners.ContainsKey(eventId))
        {
            listeners[eventId] -= callback;
        }
    }

    void UnregisterAllImpl() => listeners.Clear();

    void CallImpl(Event eventId)
    {
        if (listeners.ContainsKey(eventId) && listeners[eventId] != null)
        {
            listeners[eventId]();
        }
    }
}


/*
// This is the implementation for a single param of any type. Add a {T1,T2} 
// implementation for 2 params or {T1,T2,T3} for 3 params, etc.
public class GameEvent<T> : Singleton<GameEvent<T>>
{
    readonly IDictionary<GameEvent, Action<T>> _listeners = new SortedDictionary<GameEvent, Action<T>>();

    public static void Register(Event eventId, Action<T> callback) => Instance.RegisterImpl(eventId, callback);

    public static void Unregister(Event eventId, Action<T> callback) => Instance.UnregisterImpl(eventId, callback);

    public static void UnregisterAll() => Instance.UnregisterAllImpl();

    public static void Call(Event eventId) => Instance.CallImpl(eventId);

    void RegisterImpl(Event eventId, Action<T> callback)
    {
        if (!listeners.ContainsKey(eventId))
        {
            listeners.Add(eventId, callback);
        }
        else
        {
            listeners[eventId] += callback;
        }
    }

    void UnregisterImpl(Event eventId, Action<T> callback)
    {
        if (listeners.ContainsKey(eventId))
        {
            listeners[eventId] -= callback;
        }
    }

    void UnregisterAllImpl() => listeners.Clear();

    void CallImpl(Event eventId)
    {
        if (listeners.ContainsKey(eventId) && listeners[eventId] != null)
        {
            listeners[eventId]();
        }
    }
}
*/