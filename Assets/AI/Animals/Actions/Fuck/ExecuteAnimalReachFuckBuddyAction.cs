using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "FSM/Actions/Animal/Fuck/Reach/Execute")]
public class ExecuteAnimalReachFuckBuddyAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        var asm = (AnimalStateMachine)stateMachine;
        var navAgent = asm.GetComponent<NavMeshAgent>();

        if (navAgent.velocity.sqrMagnitude <= 0.1f)
        {
            var tasm = BreedingManager.Instance.GetOtherParentASM(asm.ID);

            Vector3 currentPosition = asm.transform.position;
            Vector3 fuckTargetPosition = tasm.transform.position;
            Vector3 meetingPoint = new()
            {
                x = (currentPosition.x + fuckTargetPosition.x) / 2,
                y = 0,
                z = (currentPosition.z + fuckTargetPosition.z) / 2
            };

            navAgent.SetDestination(meetingPoint);
            navAgent.isStopped = false;
        }
    }
}
