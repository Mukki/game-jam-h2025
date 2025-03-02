using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Animal/Fuck/Search/Execute")]
public class ExecuteAnimalSearchFuckBuddyAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        var asm = (AnimalStateMachine)stateMachine;
        asm.IncrementSearchFuckCounter();

        if (asm.FuckTarget == null)
        {
            Vector3 currentPosition = asm.transform.position;
            Vector3 forward = asm.transform.forward;
            var possibleTargets = Physics.OverlapSphere(currentPosition + forward * asm.HormoneRange.Offset, 
                asm.HormoneRange.Radius, LayerMask.GetMask("Animal"))
                .Where(x => x.gameObject != asm.gameObject);

            foreach (var target in possibleTargets)
            {
                if (target.TryGetComponent<AnimalStateMachine>(out var tasm) && tasm.IsFuckable(asm.AnimalType))
                {
                    asm.FuckTarget = tasm.gameObject;
                    asm.WillSpawnBaby = true;

                    tasm.FuckTarget = asm.gameObject;
                    tasm.WillSpawnBaby = false;
                    tasm.ForceToFuck = true;
                }
            }
        }
    }
}