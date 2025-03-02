using UnityEngine;
using System.Collections;

public class CreepyHandController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float wiggleAmount = 0.2f;
    public int wiggleCount = 2;
    public float retreatSpeed = 5f;

    private Vector3 initialPosition;
    private bool isInterrupted = false;
    private Transform target;

    void Start()
    {
        initialPosition = transform.position;
    }

    public void Grab(GameObject newTarget)
    {
        if (target != null) return; // Ignore if already grabbing
        target = newTarget.transform;
        isInterrupted = false;
        StopAllCoroutines();
        StartCoroutine(MoveToTarget());
    }

    public void Interrupt()
    {
        isInterrupted = true;
    }

    IEnumerator MoveToTarget()
    {
        while (!isInterrupted && target && Vector3.Distance(transform.position, target.position) > 0.05f)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        if (!isInterrupted && target)
        {
            yield return StartCoroutine(WiggleHand());
            Destroy(target.gameObject);
        }

        yield return StartCoroutine(MoveToPosition(initialPosition, retreatSpeed));
        target = null;
    }

    IEnumerator WiggleHand()
    {
        for (int i = 0; i < wiggleCount; i++)
        {
            transform.position += transform.right * wiggleAmount;
            yield return new WaitForSeconds(0.1f);
            transform.position -= transform.right * wiggleAmount;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator MoveToPosition(Vector3 destination, float speed)
    {
        while (Vector3.Distance(transform.position, destination) > 0.05f)
        {
            transform.position = Vector3.Lerp(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }
    }
}