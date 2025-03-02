using UnityEngine;

[CreateAssetMenu(fileName = "DayEventNewAnimal", menuName = "Scriptable Objects/DayEvents/NewAnimal")]
public class DayEventNewAnimal : DayEventBase
{
    public AnimalTypes newAnimalType;

    public override void OnUnlock()
    {
        StuffSpawner.Instance.availableAnimals.Add(newAnimalType);
    }
}
