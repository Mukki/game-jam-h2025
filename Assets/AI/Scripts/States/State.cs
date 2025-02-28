using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/State")]
public sealed class State : BaseState
{
    public List<FSMAction> EnterActions = new List<FSMAction>();
    public List<FSMAction> Actions = new List<FSMAction>();
    public List<FSMAction> ExitActions = new List<FSMAction>();

    public List<Transition> Transitions = new List<Transition>();

    public override void Enter(BaseStateMachine machine)
    {
        foreach (var enter in EnterActions)
            enter.Execute(machine);
    }
    
    public override void Exit(BaseStateMachine machine)
    {
        foreach (var exit in ExitActions)
            exit.Execute(machine);
    }

    public override void Execute(BaseStateMachine machine)
    {
        foreach (var action in Actions)
            action.Execute(machine);

        foreach (var transition in Transitions)
            transition.Execute(machine);
    }
}