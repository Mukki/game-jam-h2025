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
    MoneyChanged,
    MoneyPreviewReceived,
    SpawnAnimal,
}

// This implementation does not support params, to add params, see the exemple below
public class GameEvent
{
    readonly IDictionary<Event, Action> _listeners = new SortedDictionary<Event, Action>();
    private static GameEvent _instance;
    private static readonly object _lock = new();

    public static GameEvent Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    _instance ??= new GameEvent();
                }
            }
            return _instance;
        }
    }

    // Call this method in OnEnable() of another gameObject, see exemple below
    public static void Register(Event eventId, Action callback) => Instance.RegisterImpl(eventId, callback);

    // Call this method in OnDisable() of another gameObject, see exemple below
    public static void Unregister(Event eventId, Action callback) => Instance.UnregisterImpl(eventId, callback);

    public static void UnregisterAll() => Instance.UnregisterAllImpl();

    // Call this if you want to raise an action like so:
    // GameEvent.Call(Event.LevelStart);
    public static void Call(Event eventId) => Instance.CallImpl(eventId);

    void RegisterImpl(Event eventId, Action callback)
    {
        if (!_listeners.ContainsKey(eventId))
        {
            _listeners.Add(eventId, callback);
        }
        else
        {
            _listeners[eventId] += callback;
        }
    }

    void UnregisterImpl(Event eventId, Action callback)
    {
        if (_listeners.ContainsKey(eventId))
        {
            _listeners[eventId] -= callback;
        }
    }

    void UnregisterAllImpl() => _listeners.Clear();

    void CallImpl(Event eventId)
    {
        if (_listeners.ContainsKey(eventId) && _listeners[eventId] != null)
        {
            _listeners[eventId]();
        }
    }
}

// This is the implementation for a single param of any type. Add a {T1,T2} 
// implementation for 2 params or {T1,T2,T3} for 3 params, etc.
public class GameEvent<T>
{
    readonly IDictionary<Event, Action<T>> _listeners = new SortedDictionary<Event, Action<T>>();
    private static GameEvent<T> _instance;
    private static readonly object _lock = new();

    public static GameEvent<T> Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    _instance ??= new GameEvent<T>();
                }
            }
            return _instance;
        }
    }

    public static void Register(Event eventId, Action<T> callback) => Instance.RegisterImpl(eventId, callback);

    public static void Unregister(Event eventId, Action<T> callback) => Instance.UnregisterImpl(eventId, callback);

    public static void UnregisterAll() => Instance.UnregisterAllImpl();

    public static void Call(Event eventId, T param) => Instance.CallImpl(eventId, param);

    void RegisterImpl(Event eventId, Action<T> callback)
    {
        if (!_listeners.ContainsKey(eventId))
        {
            _listeners.Add(eventId, callback);
        }
        else
        {
            _listeners[eventId] += callback;
        }
    }

    void UnregisterImpl(Event eventId, Action<T> callback)
    {
        if (_listeners.ContainsKey(eventId))
        {
            _listeners[eventId] -= callback;
        }
    }

    void UnregisterAllImpl() => _listeners.Clear();

    void CallImpl(Event eventId, T param)
    {
        if (_listeners.ContainsKey(eventId) && _listeners[eventId] != null)
        {
            _listeners[eventId](param);
        }
    }
}

public class GameEvent<T1,T2>
{
    readonly IDictionary<Event, Action<T1,T2>> _listeners = new SortedDictionary<Event, Action<T1,T2>>();
    private static GameEvent<T1,T2> _instance;
    private static readonly object _lock = new();

    public static GameEvent<T1,T2> Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    _instance ??= new GameEvent<T1,T2>();
                }
            }
            return _instance;
        }
    }

    public static void Register(Event eventId, Action<T1,T2> callback) => Instance.RegisterImpl(eventId, callback);

    public static void Unregister(Event eventId, Action<T1,T2> callback) => Instance.UnregisterImpl(eventId, callback);

    public static void UnregisterAll() => Instance.UnregisterAllImpl();

    public static void Call(Event eventId, T1 param1, T2 param2) => Instance.CallImpl(eventId, param1, param2);

    void RegisterImpl(Event eventId, Action<T1,T2> callback)
    {
        if (!_listeners.ContainsKey(eventId))
        {
            _listeners.Add(eventId, callback);
        }
        else
        {
            _listeners[eventId] += callback;
        }
    }

    void UnregisterImpl(Event eventId, Action<T1,T2> callback)
    {
        if (_listeners.ContainsKey(eventId))
        {
            _listeners[eventId] -= callback;
        }
    }

    void UnregisterAllImpl() => _listeners.Clear();

    void CallImpl(Event eventId, T1 param1, T2 param2)
    {
        if (_listeners.ContainsKey(eventId) && _listeners[eventId] != null)
        {
            _listeners[eventId](param1, param2);
        }
    }
}