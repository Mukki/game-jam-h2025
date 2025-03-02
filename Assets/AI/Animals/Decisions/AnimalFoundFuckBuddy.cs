using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/Animal/FoundFuckBuddy")]
public class AnimalFoundFuckBuddy : Decision
{
    public override bool Decide(BaseStateMachine stateMachine)
    {
        var asm = (AnimalStateMachine)stateMachine;
        return BreedingManager.Instance.IsInAnyCouple(asm.ID);
    }
}
