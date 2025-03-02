using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "FSM/Actions/Animal/Baited/Execute")]
public class ExecuteAnimalBaitedAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        var asm = (AnimalStateMachine)stateMachine;

        if (asm.FoodTarget != null)
        {
            NavMeshAgent navAgent = asm.GetComponent<NavMeshAgent>();
            float distance = Vector3.Distance(asm.transform.position, asm.FoodTarget.transform.position);

            if (!navAgent.isStopped && distance < asm.StoppingDistanceOffset)
            {
                navAgent.isStopped = true;
                navAgent.ResetPath();
            }
        }
    }
}