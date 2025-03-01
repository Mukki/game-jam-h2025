using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/Animal/RandomTo/Idle")]
public class RandomToIdleDecision : Decision
{
    public override bool Decide(BaseStateMachine stateMachine)
    {
        var asm = (AnimalStateMachine)stateMachine;
        return asm.RandomAnimalState == AnimalState.Idle;
    }
}
