using UnityEngine;

public class ShadowHandController : MonoBehaviour
{
    public Transform target; // Object to grab
    public float speed = 2f;
    public float grabSpeed = 5f;
    public float retreatSpeed = 3f;
    public float dissolveSpeed = 2f;
    
    private Material handMaterial;
    private float dissolveAmount = 0f;
    private bool grabbing = false;
    private bool retreating = false;
    private Vector3 startPosition;
    private Camera mainCamera;

    void Start()
    {
        handMaterial = GetComponentInChildren<SpriteRenderer>().material;
        startPosition = transform.position;
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (target == null)
        {
            return;
        }

        if (!grabbing && !retreating)
        {
            // Move towards target
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
            dissolveAmount = Mathf.Lerp(dissolveAmount, 0f, Time.deltaTime * dissolveSpeed);
            handMaterial.SetFloat("_DissolveAmount", dissolveAmount);

            if (Vector3.Distance(transform.position, target.position) < 0.2f)
            {
                grabbing = true;
            }
        }

        if (grabbing)
        {
            // Move faster toward the target
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * grabSpeed);
            

            if (Vector3.Distance(transform.position, target.position) < 0.2f)
            {
                grabbing = false;
                retreating = true;
            }
        }

        if (retreating)
        {
            target.SetParent(transform);
            transform.position = Vector3.Lerp(transform.position, startPosition, Time.deltaTime * retreatSpeed);
            dissolveAmount = Mathf.Lerp(dissolveAmount, 1f, Time.deltaTime * dissolveSpeed);
            handMaterial.SetFloat("_DissolveAmount", dissolveAmount);

            if (Vector3.Distance(transform.position, startPosition) < 0.5f)
            {
                retreating = false;
                Destroy(target.gameObject);
                target = null;
            }
        }
    }
    
    void LateUpdate()
    {
        transform.rotation = mainCamera.transform.rotation;
    }

    public void Grab()
    {
        grabbing = true;
    }
}