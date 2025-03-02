using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "FSM/Actions/Animal/Wander/Exit")]
public class ExitAnimalWanderAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        var asm = (AnimalStateMachine)stateMachine;
        NavMeshAgent navAgent = asm.GetComponent<NavMeshAgent>();

        navAgent.isStopped = true;
    }
}
