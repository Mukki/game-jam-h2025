using TMPro;
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
        NavMeshPath navMeshPath = new();
        Vector3 targetPosition;
        Vector3 currentPosition = asm.transform.position;

        int failSafe = 10;

        do
        {
            float x = Random.Range(-asm.RandomWanderRange, asm.RandomWanderRange) + currentPosition.x;
            float y = asm.transform.position.y;
            float z = Random.Range(-asm.RandomWanderRange, asm.RandomWanderRange) + currentPosition.z;
            targetPosition = new(x, y, z);
            navAgent.SetDestination(targetPosition);
            asm.transform.LookAt(targetPosition);
            failSafe--;
        } while ((!navAgent.CalculatePath(targetPosition, navMeshPath) || navMeshPath.status != NavMeshPathStatus.PathComplete) && failSafe > 0);

        navAgent.isStopped = false;
    }
}
