using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/Animal/HasReachFuckBuddy")]
public class AnimalHasReachFuckBuddy : Decision
{
    public override bool Decide(BaseStateMachine stateMachine)
    {
        var asm = (AnimalStateMachine)stateMachine;
        var tasm = BreedingManager.Instance.GetOtherParentASM(asm.ID);
        float distance = Vector3.Distance(asm.transform.position, tasm.transform.position);
        return distance < asm.StoppingDistanceOffset;
    }
}