using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/Animal/ForceToFuck")]
public class ForceToFuck : Decision
{
    public override bool Decide(BaseStateMachine stateMachine)
    {
        return ((AnimalStateMachine)stateMachine).ForceToFuck;
    }
}