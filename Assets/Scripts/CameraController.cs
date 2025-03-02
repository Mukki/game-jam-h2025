using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 5.0f;
    public float zoomSpeed = 20.0f;

    public Vector2 xLimit = new Vector2(-16, 16);
    public Vector2 yLimit = new Vector2(-16, 2);
    public Vector2 zoomLimit = new Vector2(5, 15);

    private void Update()
    {
        Vector3 panDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            panDirection -= Time.deltaTime * panSpeed * Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            panDirection += Time.deltaTime * panSpeed * Vector3.forward;
        }

        if (Input.GetKey(KeyCode.W))
        {
            panDirection -= Time.deltaTime * panSpeed * Vector3.right;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            panDirection += Time.deltaTime * panSpeed * Vector3.right;
        }

        Vector3 zoomDirection = Vector3.zero;

        zoomDirection -= Input.mouseScrollDelta.y * Time.deltaTime * zoomSpeed * Vector3.up;

        Vector3 nextPosition = transform.position + panDirection + zoomDirection;

        nextPosition.x = Mathf.Clamp(nextPosition.x, yLimit.x, yLimit.y);
        nextPosition.y = Mathf.Clamp(nextPosition.y, zoomLimit.x, zoomLimit.y);
        nextPosition.z = Mathf.Clamp(nextPosition.z, xLimit.x, xLimit.y);

        transform.position = nextPosition;
    }
}
