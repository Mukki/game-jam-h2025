using UnityEngine;

[CreateAssetMenu(fileName = "ConstructionLamp", menuName = "Scriptable Objects/ConstructionLamp")]
public class ConstructionLamp : ConstructionBase
{
    public override void ProcessClick(Vector3 position)
    {
        Instantiate(prefab, position, Quaternion.identity);
    }
}
