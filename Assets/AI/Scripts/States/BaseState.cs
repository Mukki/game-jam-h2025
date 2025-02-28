using UnityEngine;

public class BaseState : ScriptableObject
{
    public string StateName;
    public virtual void Execute(BaseStateMachine machine) { }
    public virtual void Enter(BaseStateMachine machine) { }
    public virtual void Exit(BaseStateMachine machine) { }
}