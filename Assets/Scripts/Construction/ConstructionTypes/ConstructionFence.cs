using UnityEngine;

[CreateAssetMenu(fileName = "ConstructionFence", menuName = "Scriptable Objects/Constructions/Fence")]
public class ConstructionFence : ConstructionBase
{
    private bool firstPosSet = false;
    private Vector3 firstPos = Vector3.zero;

    public override void ProcessClick(Vector3 position)
    {
        if (!firstPosSet)
        {
            firstPos = position;
        }
        else
        {
            GameObject newFence = Instantiate(prefab, firstPos, Quaternion.identity);

            Vector3 newScale = newFence.transform.localScale;
            newScale.x = Vector3.Distance(firstPos, position);
            newFence.transform.localScale = newScale;

            Vector3 newRotation = newFence.transform.eulerAngles;
            newRotation.y = Vector3.SignedAngle(Vector3.right, position - firstPos, Vector3.up);
            newFence.transform.eulerAngles = newRotation;
        }

        firstPosSet = !firstPosSet;
    }
}
