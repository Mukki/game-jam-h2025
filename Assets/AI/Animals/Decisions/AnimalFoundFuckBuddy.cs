using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/Animal/FoundFuckBuddy")]
public class AnimalFoundFuckBuddy : Decision
{
    public override bool Decide(BaseStateMachine stateMachine)
    {
        return ((AnimalStateMachine)stateMachine).FuckTarget != null;
    }
}
