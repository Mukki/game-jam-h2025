using UnityEngine;

public class ExitAnimalReachFuckBuddyAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        var asm = (AnimalStateMachine)stateMachine;
        asm.RandomAnimalState = AnimalState.None;

        if (asm.WillSpawnBaby)
        {
            // Trigger spawn baby game event
        }
    }
}
