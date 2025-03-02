using UnityEngine;

[CreateAssetMenu(fileName = "DayEventConstructionUnlock", menuName = "Scriptable Objects/DayEvents/ConstructionUnlock")]
public class DayEventConstructionUnlock : DayEventBase
{
    public ConstructionBase newConstruction;

    public override void OnUnlock()
    {
        ConstructionManager.Instance.availableConstructions.Add(newConstruction);
    }
}
