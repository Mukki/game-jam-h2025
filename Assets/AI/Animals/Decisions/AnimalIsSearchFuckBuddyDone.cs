using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/Animal/IsSearchFuckBuddyDone")]
public class AnimalIsSearchFuckBuddyDone : Decision
{
    public override bool Decide(BaseStateMachine state)
    {
        var asm = (AnimalStateMachine)state;
        return asm.IsSearchFuckCounterDone() || !asm.ThisCanFuck();

    }
}