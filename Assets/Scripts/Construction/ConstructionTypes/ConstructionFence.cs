using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConstructionFence", menuName = "Scriptable Objects/Constructions/Fence")]
public class ConstructionFence : ConstructionBase
{
    public bool firstPosSet = false;
    private List<GameObject> fences = new List<GameObject>();
    private Vector3 firstPos = Vector3.zero;

    public override void Init()
    {
        firstPosSet = false;
        fences = new List<GameObject>();
        firstPos = Vector3.zero;
    }

    public override void OnMouseEnter()
    {

    }

    public override void OnMouseLeave()
    {
        if (!firstPosSet)
        {
            ClearGhostFences();
            GameEvent<float>.Call(Event.MoneyPreviewReceived, 0);
        }
    }

    public override void ProcessClick(Vector3 position)
    {
        if (!firstPosSet)
        {
            firstPos = position;
        }
        else
        {
            TryConstruct(pricePerUnit * fences.Count);
        }

        firstPosSet = !firstPosSet;
    }

    public override void ProcessMove(Vector3 position)
    {
        if (!firstPosSet)
        {
            firstPos = position;
        }

        UpdateGameObject(position);
        GameEvent<float>.Call(Event.MoneyPreviewReceived, pricePerUnit * fences.Count);
    }

    public override void ProcessCancel()
    {
        firstPosSet = false;
        ClearGhostFences();
    }

    public override void OnConstruct()
    {
        foreach (GameObject fence in fences)
        {
            GameObject newFence = Instantiate(prefab, fence.transform.position, fence.transform.rotation, ConstructionManager.Instance.constructionsParent);
            ConstructionManager.Instance.allFences.Add(newFence);
        }
    }

    private void UpdateGameObject(Vector3 targetPos)
    {
        float targetLength = Mathf.Max(1.0f, Mathf.Ceil(Vector3.Distance(firstPos, targetPos)));

        while (fences.Count < targetLength)
        {
            fences.Add(Instantiate(ghostPrefab, ConstructionManager.Instance.constructionsParent));
        }

        while (fences.Count > targetLength)
        {
            Destroy(fences[fences.Count - 1]);
            fences.RemoveAt(fences.Count - 1);
        }

        Vector3 position = firstPos;

        Vector3 newRotation = Vector3.zero;
        newRotation.y = Vector3.SignedAngle(Vector3.right, targetPos - firstPos, Vector3.up);

        foreach (GameObject fence in fences)
        {
            fence.transform.position = position;
            fence.transform.eulerAngles = newRotation;

            position.x += Mathf.Cos(Mathf.Deg2Rad * -newRotation.y);
            position.z += Mathf.Sin(Mathf.Deg2Rad * -newRotation.y);
        }
    }

    private void ClearGhostFences()
    {
        foreach (GameObject fence in fences)
        {
            Destroy(fence);
        }

        fences.Clear();
    }
}
