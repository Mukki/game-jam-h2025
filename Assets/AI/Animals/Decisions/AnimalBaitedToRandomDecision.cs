using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/Animal/BaitedToRandom")]
public class AnimalBaitedToRandomDecision : Decision
{
    public override bool Decide(BaseStateMachine stateMachine)
    {
        var asm = (AnimalStateMachine)stateMachine;
        return asm.FoodTarget == null;
    }
}
