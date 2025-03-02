using System;
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

    public float RandomWanderRange = 1.5f;
    public float StoppingDistanceOffset = 0.5f;
    public int MaxBabyPerDay = 2;

    public FieldOfView ViewRange = new()
    {
        Radius = 3,
        Offset = 2
    };

    public FieldOfView HormoneRange = new()
    {
        Radius = 4,
        Offset = 1,
    };

    public FieldOfView SmellRange = new()
    {
        Radius = 5,
        Offset = 1,
    };

    public BoundedValues HornyTimeRange = new()
    {
        Min = 200,
        Max = 250,
    };

    public BoundedValues IdleTimeRange = new()
    {
        Min = 100,
        Max = 150
    };
}
