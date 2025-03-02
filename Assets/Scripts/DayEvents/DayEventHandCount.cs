using UnityEngine;

[CreateAssetMenu(fileName = "DayEventHandCount", menuName = "Scriptable Objects/DayEvents/HandCount")]
public class DayEventHandCount : DayEventBase
{
    public int addedHandCount;

    public override void OnUnlock()
    {
        ShadowHandManager.Instance.handCount += addedHandCount;
    }
}
