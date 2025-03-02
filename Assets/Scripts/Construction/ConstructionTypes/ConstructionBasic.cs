using UnityEngine;

[CreateAssetMenu(fileName = "ConstructionBasic", menuName = "Scriptable Objects/Constructions/Basic")]
public class ConstructionBasic : ConstructionBase
{
    private GameObject ghostPreview = null;
    private Vector3 targetPosition = Vector3.zero;

    public override void Init()
    {
        ghostPreview = null;
        targetPosition = Vector3.zero;
    }

    public override void OnMouseEnter()
    {
        ghostPreview = Instantiate(ghostPrefab, ConstructionManager.Instance.constructionsParent);
    }

    public override void OnMouseLeave()
    {
        Destroy(ghostPreview);
        ghostPreview = null;
        GameEvent<float>.Call(Event.MoneyPreviewReceived, 0);
    }

    public override void ProcessClick(Vector3 position)
    {
        targetPosition = position;
        TryConstruct(pricePerUnit);
    }

    public override void ProcessMove(Vector3 position)
    {
        ghostPreview.transform.position = position;
        GameEvent<float>.Call(Event.MoneyPreviewReceived, pricePerUnit);
    }

    public override void ProcessCancel()
    {

    }

    public override void OnConstruct()
    {
        GameObject newObject = Instantiate(prefab, targetPosition, Quaternion.identity, ConstructionManager.Instance.constructionsParent);

        Vector3 newRotation = Vector3.zero;
        newRotation.y = Random.Range(0, 360);
        newObject.transform.eulerAngles = newRotation;
    }
}
