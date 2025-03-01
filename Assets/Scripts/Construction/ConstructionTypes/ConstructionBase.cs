using UnityEngine;

public abstract class ConstructionBase : ScriptableObject
{
    public float pricePerUnit;
    public GameObject prefab;
    public Sprite Image;
    public string Name;

    public abstract void ProcessClick(Vector3 position);
    public abstract void ProcessCancel();
    //public abstract void OnConstruct();

    protected bool CanConstruct(float price)
    {
        return GameManager.Instance.money >= price;
    }

    protected void Construct()
    {
        if (CanConstruct(pricePerUnit))
        {
            //OnConstruct();
            //FinalizeConstruct();
        }
    }

    protected void FinalizeConstruct(float price)
    {
        
    }
}
