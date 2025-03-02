using UnityEngine;

public class SummaryInterface : MonoBehaviour
{
    public GameObject parentContainer;
    public GameObject summaryObj;
    public Sprite MoneySprite;

    public void OnContinue()
    {
        GameManager.Instance.OnEndOfNightCallback();
    }
}
