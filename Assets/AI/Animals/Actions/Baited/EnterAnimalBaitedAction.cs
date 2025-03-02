using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "FSM/Actions/Animal/Baited/Enter")]
public class EnterAnimalBaitedAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        var asm = (AnimalStateMachine)stateMachine;
        asm.RandomAnimalState = AnimalState.None;

        Vector3 currentPosition = asm.transform.position;
        Vector3 forward = asm.transform.forward;

        var possibleTargets = Physics.OverlapSphere(currentPosition + forward * asm.SmellRange.Offset,
            asm.SmellRange.Radius, LayerMask.GetMask("Food"));

        if (possibleTargets.Any())
        {
            asm.FoodTarget = possibleTargets.FirstOrDefault().gameObject;
            NavMeshAgent navAgent = asm.GetComponent<NavMeshAgent>();

            Vector3 foodPosition = asm.FoodTarget.transform.position;
            float x = foodPosition.x;
            float z = foodPosition.z;
            Vector3 targetPosition = new(x, 0, z);

            navAgent.SetDestination(targetPosition);
            navAgent.isStopped = false;
        }
        else
        {
            asm.FoodTarget = null;
        }
    }
}
