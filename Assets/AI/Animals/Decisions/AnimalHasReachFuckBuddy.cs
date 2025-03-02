using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/Animal/HasReachFuckBuddy")]
public class AnimalHasReachFuckBuddy : Decision
{
    public override bool Decide(BaseStateMachine stateMachine)
    {
        var asm = (AnimalStateMachine)stateMachine;

        // Sometimes, the ASM can loose its target for any reason,
        // if that's the case, end the state
        if (asm.FuckTarget == null)
            return true;

        if (asm.FuckTarget.TryGetComponent<AnimalStateMachine>(out var target) && target.FuckTarget == null)
            return true;

        float distance = Vector3.Distance(asm.transform.position, asm.FuckTarget.transform.position);
        return distance < asm.StoppingDistanceOffset;
    }
}