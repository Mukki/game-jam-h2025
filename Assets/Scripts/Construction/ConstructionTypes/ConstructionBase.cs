using UnityEngine;

public abstract class ConstructionBase : ScriptableObject
{
    public GameObject prefab;
    public string Name;
    public Sprite Image;

    public abstract void ProcessClick(Vector3 position);
}
