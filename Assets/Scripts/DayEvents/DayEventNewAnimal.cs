using UnityEngine;

[CreateAssetMenu(fileName = "DayEventNewAnimal", menuName = "Scriptable Objects/DayEvents/NewAnimal")]
public class DayEventNewAnimal : DayEventBase
{
    public GameObject animalPrefab;

    public override void OnUnlock()
    {

    }
}
