using UnityEngine;

[CreateAssetMenu(fileName = "ConstructionLamp", menuName = "Scriptable Objects/Constructions/Lamp")]
public class ConstructionLamp : ConstructionBase
{
    public override void ProcessClick(Vector3 position)
    {
        Instantiate(prefab, position, Quaternion.identity);
    }
}
