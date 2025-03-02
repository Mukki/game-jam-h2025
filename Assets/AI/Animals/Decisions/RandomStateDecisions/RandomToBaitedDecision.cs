using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/Animal/RandomTo/Baited")]
public class RandomToBaitedDecision : Decision
{
    public override bool Decide(BaseStateMachine stateMachine)
    {
        var asm = (AnimalStateMachine)stateMachine;
        return asm.RandomAnimalState == AnimalState.Baited;
    }
}
