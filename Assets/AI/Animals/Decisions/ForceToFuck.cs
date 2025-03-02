using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/Animal/ForceToFuck")]
public class ForceToFuck : Decision
{
    public override bool Decide(BaseStateMachine stateMachine)
    {
        var asm = (AnimalStateMachine)stateMachine;
        return BreedingManager.Instance.IsInAnyCouple(asm.ID);
    }
}