using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Animal/Idle/Execute")]
public class AnimalIdleAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine) => ((AnimalStateMachine)stateMachine).IncrementCounter();
}