using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnlockPanel : MonoBehaviour
{
    public Image Image;
    public TMP_Text Name;
    public TMP_Text Description;
    
    public void OnContinue()
    {
        GameManager.Instance.OnStartOfDayCallback();
    }
}
