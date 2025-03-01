using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/Animal/RandomTo/Fuck")]
public class RandomToFuckDecision : Decision
{
    public override bool Decide(BaseStateMachine stateMachine)
    {
        var asm = (AnimalStateMachine)stateMachine;
        return asm.RandomAnimalState == AnimalState.WantsToFuck;
    }
}
