using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "FSM/Decisions/Animal/HasReachedDestination")]
public class AnimalHasReachedDestination : Decision
{
    public override bool Decide(BaseStateMachine stateMachine)
    {
        var asm = (AnimalStateMachine)stateMachine;
        var agent = asm.GetComponent<NavMeshAgent>();

        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f);
    }
}
