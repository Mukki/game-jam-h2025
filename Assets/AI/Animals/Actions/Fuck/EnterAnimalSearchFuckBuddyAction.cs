using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Animal/Fuck/Search/Enter")]
public class EnterAnimalSearchFuckBuddyAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        var asm = (AnimalStateMachine)stateMachine;
        asm.ResetSearchFuckCounter();
        asm.FuckTarget = null;
        asm.WillSpawnBaby = false;
    }
}