using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/Animal/IsIdleDone")]
public class AnimalIsIdleDone : Decision
{
    public override bool Decide(BaseStateMachine state) => ((AnimalStateMachine)state).IsIdleCounterDone();
}
