using UnityEngine;

public abstract class ConstructionBase : ScriptableObject
{
    public GameObject prefab;

    public abstract void ProcessClick(Vector3 position);
}
