using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/Animal/ForceToSleep")]
public class ForceToSleep : Decision
{
    public override bool Decide(BaseStateMachine stateMachine) 
        => ((AnimalStateMachine)stateMachine).ForceToSleep;
}
