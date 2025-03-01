using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "ConstructionFence", menuName = "Scriptable Objects/Constructions/Fence")]
public class ConstructionFence : ConstructionBase
{
    private bool firstPosSet = false;
    private Vector3 firstPos = Vector3.zero;
    private Vector3 secondPos = Vector3.zero;

    public override void ProcessClick(Vector3 position)
    {
        if (!firstPosSet)
        {
            firstPos = position;
        }
        else
        {
            secondPos = position;
            float length = Vector3.Distance(firstPos, position);

            TryConstruct(pricePerUnit * length);
        }

        firstPosSet = !firstPosSet;
    }

    public override void ProcessMove(Vector3 position)
    {
        if (!firstPosSet)
        {
            ConstructionManager.Instance.ghostPreview.transform.position = position;
        }
        else
        {
            UpdateGameObject(ConstructionManager.Instance.ghostPreview, position);
        }
    }

    public override void ProcessCancel()
    {
        firstPosSet = false;
    }

    public override void OnConstruct()
    {
        GameObject newFence = Instantiate(prefab, firstPos, Quaternion.identity);

        UpdateGameObject(newFence, secondPos);
    }

    private void UpdateGameObject(GameObject go, Vector3 targetPos)
    {
        float length = Vector3.Distance(firstPos, targetPos);

        Vector3 newScale = go.transform.localScale;
        newScale.x = length;
        go.transform.localScale = newScale;

        Vector3 newRotation = go.transform.eulerAngles;
        newRotation.y = Vector3.SignedAngle(Vector3.right, targetPos - firstPos, Vector3.up);
        go.transform.eulerAngles = newRotation;
    }
}
