using UnityEngine;
using System.Collections;

public class ShadowHandController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float wiggleAmount = 0.2f;
    public int wiggleCount = 2;
    public float wiggleAngle = 10f;
    public float retreatSpeed = 5f;
    public float wiggleDuration = 0.1f;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private bool isInterrupted = false;
    private Transform target;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        initialPosition = transform.position;
        initialRotation = transform.rotation;
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
            Quaternion leftTilt = Quaternion.Euler(0, 0, wiggleAngle);
            Quaternion rightTilt = Quaternion.Euler(0, 0, -wiggleAngle);

            yield return StartCoroutine(RotateTo(leftTilt * transform.rotation, wiggleDuration));
            yield return StartCoroutine(RotateTo(rightTilt * transform.rotation, wiggleDuration));
        }
        
        yield return StartCoroutine(RotateTo(initialRotation, wiggleDuration)); // Reset rotation
    }

    IEnumerator MoveToPosition(Vector3 destination, float speed)
    {
        while (Vector3.Distance(transform.position, destination) > 0.05f)
        {
            transform.position = Vector3.Lerp(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }
    }
    
    IEnumerator RotateTo(Quaternion targetRotation, float duration)
    {
        float timeElapsed = 0;
        Quaternion startRotation = transform.rotation;

        while (timeElapsed < duration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
    }

    void LateUpdate()
    {
        transform.rotation = mainCamera.transform.rotation;
    }
}