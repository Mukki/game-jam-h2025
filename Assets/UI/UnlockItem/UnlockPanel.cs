using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UnlockPanel : MonoBehaviour
{

    private IEnumerator _coroutine;

    public Image Image;
    public TMP_Text Name;
    public TMP_Text Description;
    
    public void OnContinue()
    {
        SoundManager.Instance.MySource.clip = SoundManager.Instance.MorningQueue;
        SoundManager.Instance.MySource.loop = false;
        SoundManager.Instance.MySource.Play();
        _coroutine = MorningQueue(3.0f);
        StartCoroutine(_coroutine);
    }

    private IEnumerator MorningQueue(float wait)
    {
        yield return new WaitForSeconds(wait);
        GameManager.Instance.OnStartOfDayCallback();
    }
}
