using UnityEngine;

public abstract class ConstructionBase : ScriptableObject
{
    public float pricePerUnit;
    public GameObject prefab;
    public GameObject ghostPrefab;
    public Sprite Image;
    public string Name;

    public abstract void ProcessClick(Vector3 position);
    public abstract void ProcessMove(Vector3 position);
    public abstract void ProcessCancel();
    public abstract void OnMouseEnter();
    public abstract void OnMouseLeave();

    public abstract void OnConstruct();
    public abstract void Init();

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
