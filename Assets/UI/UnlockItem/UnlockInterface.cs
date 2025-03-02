using UnityEngine;

public class UnlockInterface : MonoBehaviour
{
    public GameObject parentContainer;
    public UnlockPanel unlockInfo;

    public void Display(DayEventBase dayEvent)
    {
        unlockInfo.Description.text = dayEvent.description;
    }
}
