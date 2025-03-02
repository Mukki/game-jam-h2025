using UnityEngine;

public class UnlockInterface : MonoBehaviour
{
    public GameObject parentContainer;
    public UnlockPanel unlockInfo;

    public void Display(DayEventBase dayEvent)
    {
        unlockInfo.Name.text = dayEvent.title;
        unlockInfo.Description.text = dayEvent.description;
    }
}
