using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "FSM/Actions/Animal/Idle/Enter")]
public class AnimalIdleEnterAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        var asm = (AnimalStateMachine)stateMachine;
        asm.ResetIdleCounter();

        var navAgent = asm.GetComponent<NavMeshAgent>();
        navAgent.isStopped = true;
        navAgent.ResetPath();
        navAgent.velocity = Vector3.zero;

    }
}
