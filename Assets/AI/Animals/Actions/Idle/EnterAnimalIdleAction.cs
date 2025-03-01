using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Animal/Idle/Enter")]
public class AnimalIdleEnterAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        var asm = (AnimalStateMachine)stateMachine;
        asm.ResetIdleCounter();
    }
}
