using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Animal/Generic/Exit")]
public class GenericExitAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        var asm = (AnimalStateMachine)stateMachine;
        asm.RandomAnimalState = AnimalState.None;
    }
}
