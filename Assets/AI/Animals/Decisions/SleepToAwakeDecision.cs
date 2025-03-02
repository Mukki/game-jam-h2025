using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/Animal/SleepToAwake")]
public class SleepToAwakeDecision : Decision
{
    public override bool Decide(BaseStateMachine stateMachine)
        => !((AnimalStateMachine)stateMachine).ForceToSleep;
}
