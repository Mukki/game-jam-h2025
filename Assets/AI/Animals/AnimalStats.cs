using UnityEngine;

public enum AnimalTypes
{
    Pig,
    Cow,
    Chicken
}

[CreateAssetMenu(fileName = "AnimalStats", menuName = "Scriptable Objects/AnimalStats")]
public class AnimalStats : ScriptableObject
{
    public AnimalTypes AnimalType;

    public float FieldOfViewRange = 2f;
    public float FieldOfViewOffset = 3f;
    public float RandomWanderRange = 1.5f;

    public BoundedValues IdleTimeRange = new()
    {
        Min = 100,
        Max = 150
    };
}
