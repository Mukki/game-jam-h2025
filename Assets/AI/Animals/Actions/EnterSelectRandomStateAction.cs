using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Animal/SelectRandomState/Enter")]
public class EnterSelectRandomStateAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        var asm = (AnimalStateMachine)stateMachine;

        // Gets the highest state value
        int maxRandomValue = (int)Enum.GetValues(typeof(AnimalState)).Cast<AnimalState>().Max();
        // Add +1, because 0 is the None state, which isn't an option and maxRandomValue is excluded from Random
        int selectedState = UnityEngine.Random.Range(0, maxRandomValue) + 1;

        asm.RandomAnimalState = (AnimalState)selectedState;
    }
}
