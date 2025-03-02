using UnityEngine;

public abstract class ConstructionBase : ScriptableObject
{
    public float pricePerUnit;
    public GameObject prefab;
    public GameObject ghostPrefab;
    public Sprite Image;
    public string Name;

    protected GameObject ghostPreview;

    public abstract void ProcessClick(Vector3 position);
    public abstract void ProcessMove(Vector3 position);
    public abstract void ProcessCancel();

    public abstract void OnConstruct();

    protected bool CanConstruct(float price)
    {
        return GameManager.Instance.Money >= price;
    }

    protected void TryConstruct(float price)
    {
        if (CanConstruct(price))
        {
            ConstructionManager.Instance.ResetGhost();
            OnConstruct();

            GameManager.Instance.PayMoney(price);
        }
    }
}
