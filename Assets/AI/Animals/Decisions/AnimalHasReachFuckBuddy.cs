using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/Animal/HasReachFuckBuddy")]
public class AnimalHasReachFuckBuddy : Decision
{
    public override bool Decide(BaseStateMachine stateMachine)
    {
        var asm = (AnimalStateMachine)stateMachine;
        float distance = Vector3.Distance(asm.transform.position, asm.FuckTarget.transform.position);

        return distance < 0.5f;
    }
}