using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "FSM/Actions/Animal/Wander/Enter")]
public class EnterAnimalWanderAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        var asm = (AnimalStateMachine)stateMachine;
        asm.RandomAnimalState = AnimalState.None;

        NavMeshAgent navAgent = asm.GetComponent<NavMeshAgent>();
        Vector3 currentPosition = asm.transform.position;
        float x = Random.Range(-asm.RandomWanderRange, asm.RandomWanderRange) + currentPosition.x;
        float z = Random.Range(-asm.RandomWanderRange, asm.RandomWanderRange) + currentPosition.z;
        Vector3 targetPosition = new(x, 0, z);
        navAgent.SetDestination(targetPosition);
    }
}
