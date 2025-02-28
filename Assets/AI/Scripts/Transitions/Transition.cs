using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Transition")]
public sealed class Transition : ScriptableObject
{
    public Decision Decision;
    public BaseState TrueState;
    public BaseState FalseState;

    public void Execute(BaseStateMachine stateMachine)
    {
        if (Decision.Decide(stateMachine) && TrueState is not RemainInState)
        {
            stateMachine.CurrentState.Exit(stateMachine);
            stateMachine.CurrentState = TrueState;
            stateMachine.CurrentState.Enter(stateMachine);

        }
        else if (FalseState is not RemainInState)
        {
            stateMachine.CurrentState.Exit(stateMachine);
            stateMachine.CurrentState = FalseState;
            stateMachine.CurrentState.Enter(stateMachine);
        }
    }
}