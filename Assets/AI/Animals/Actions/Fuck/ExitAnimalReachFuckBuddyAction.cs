using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "FSM/Actions/Animal/Fuck/Reach/Exit")]
public class ExitAnimalReachFuckBuddyAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        var asm = (AnimalStateMachine)stateMachine;
        asm.RandomAnimalState = AnimalState.None;

        NavMeshAgent navAgent = asm.GetComponent<NavMeshAgent>();
        navAgent.isStopped = true;
        navAgent.ResetPath();
        navAgent.velocity = Vector3.zero;
    }
}
