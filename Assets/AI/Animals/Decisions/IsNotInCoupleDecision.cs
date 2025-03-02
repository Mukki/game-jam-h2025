using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/Animal/IsNotInCouple")]
public class IsNotInCoupleDecision : Decision
{
    public override bool Decide(BaseStateMachine stateMachine)
    {
        var asm = (AnimalStateMachine)stateMachine;

        return !BreedingManager.Instance.IsInAnyCouple(asm.ID);
    }
}
