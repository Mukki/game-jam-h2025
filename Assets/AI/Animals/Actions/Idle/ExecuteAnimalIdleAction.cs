using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "FSM/Actions/Animal/Idle/Execute")]
public class AnimalIdleAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        var asm = (AnimalStateMachine)stateMachine;
        asm.IncrementIdleCounter();

        var navAgent = asm.GetComponent<NavMeshAgent>();
        navAgent.isStopped = true;
        navAgent.ResetPath();
        navAgent.velocity = Vector3.zero;

        var rb = asm.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}