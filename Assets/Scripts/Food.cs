using System.Collections;
using UnityEngine;

public class Food : MonoBehaviour
{
    public float LifeTime = 5.0f;

    private IEnumerator _couroutine;

    private void OnEnable()
    {
        _couroutine = DayCycleCountDown(LifeTime);
        StartCoroutine(_couroutine);
    }
    private IEnumerator DayCycleCountDown(float wait)
    {
        yield return new WaitForSeconds(wait);
        Destroy(gameObject);
    }
}
