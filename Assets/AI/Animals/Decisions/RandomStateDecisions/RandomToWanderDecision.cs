using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/Animal/RandomTo/Wander")]
public class RandomToWanderDecision : Decision
{
    public override bool Decide(BaseStateMachine stateMachine)
    {
        var asm = (AnimalStateMachine)stateMachine;
        return asm.RandomAnimalState == AnimalState.Wander;
    }
}
