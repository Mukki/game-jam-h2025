using UnityEngine;

[CreateAssetMenu(fileName = "DayEventNewTerrain", menuName = "Scriptable Objects/DayEvents/NewTerrain")]
public class DayEventNewTerrain : DayEventBase
{
    public int terrainCountToAdd;

    public override void OnUnlock()
    {
        AnimalSpawner.Instance.AddSpawnableTerrains(terrainCountToAdd);
    }
}
