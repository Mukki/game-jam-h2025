using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum LevelState
{
    LevelLoading,
    LevelStarted,
    LevelInProgress,
    LevelEnded,
}

public class BaseStateMachine : MonoBehaviour
{
    [SerializeField] protected BaseState _initialState;
    public BaseState CurrentState;

    protected Dictionary<Type, Component> _cachedComponents;

    [SerializeField]
    private LevelState _levelState = LevelState.LevelLoading;

    protected virtual void OnEnable()
    {
        GameEvent.Register(Event.LevelStart, OnLevelStart);
        GameEvent.Register(Event.LevelEnd, OnLevelEnd);
    }

    protected virtual void OnDisable()
    {
        GameEvent.Unregister(Event.LevelStart, OnLevelStart);
        GameEvent.Unregister(Event.LevelEnd, OnLevelEnd);
    }

    protected virtual void Awake()
    {
        CurrentState = _initialState;
        _cachedComponents = new Dictionary<Type, Component>();

        var agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    protected virtual void FixedUpdate()
    {
        CurrentState.Execute(this);
    }

    public new T GetComponent<T>() where T : Component
    {
        if (_cachedComponents.ContainsKey(typeof(T)))
            return _cachedComponents[typeof(T)] as T;

        if (base.TryGetComponent<T>(out var component))
        {
            _cachedComponents.Add(typeof(T), component);
        }
        return component;
    }

    protected virtual void OnLevelStart()
    {
        SetLevelState(LevelState.LevelStarted);
    }

    protected virtual void OnLevelEnd()
    {
        SetLevelState(LevelState.LevelEnded);
    }

    public void SetLevelState(LevelState levelState)
    {
        _levelState = levelState;
    }

    public LevelState GetLevelState()
    {
        return _levelState;
    }
}

