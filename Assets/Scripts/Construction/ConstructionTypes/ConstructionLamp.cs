using UnityEngine;

[CreateAssetMenu(fileName = "ConstructionLamp", menuName = "Scriptable Objects/Constructions/Lamp")]
public class ConstructionLamp : ConstructionBase
{
    private Vector3 targetPosition = Vector3.zero;

    public override void ProcessClick(Vector3 position)
    {
        targetPosition = position;

        TryConstruct(pricePerUnit);
    }

    public override void ProcessMove(Vector3 position)
    {
        ConstructionManager.Instance.ghostPreview.transform.position = position;
        GameEvent<float>.Call(Event.MoneyPreviewReceived, pricePerUnit);
    }

    public override void ProcessCancel()
    {

    }

    public override void OnConstruct()
    {
        Instantiate(prefab, targetPosition, Quaternion.identity);
    }
}
