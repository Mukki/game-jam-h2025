using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "FSM/Actions/Animal/Fuck/Reach/Exit")]
public class ExitAnimalReachFuckBuddyAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        var asm = (AnimalStateMachine)stateMachine;
        asm.RandomAnimalState = AnimalState.None;

        if (asm.WillSpawnBaby)
        {
            GameEvent<AnimalTypes, Vector3>.Call(Event.SpawnAnimal, asm.AnimalType, asm.transform.position);
        }

        asm.FuckTarget = null;
        asm.WillSpawnBaby = false;
        asm.ForceToFuck = false;

        NavMeshAgent navAgent = asm.GetComponent<NavMeshAgent>();
        navAgent.isStopped = true;
    }
}
