using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class TestAiAnimal : MonoBehaviour
{
    public BoundedValues BoundedValues;
    public float Radius = 2f;
    public float ForwardRange = 2f;

    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Vector3 currentPosition = transform.position;
            Vector3 forward = transform.forward;

            var test = Physics.OverlapSphere(currentPosition + forward * ForwardRange, Radius, LayerMask.GetMask("Animal"));

            Debug.Log(test.Where(x => x.gameObject != gameObject).Count());
        }


        /*
        if (gameObject.TryGetComponent<NavMeshAgent>(out var agent))
        {
            agent.SetDestination(new Vector3 (0, 0, 0));
        }
        */
    }

    private void OnDrawGizmos()
    {
        Vector3 currentPosition = transform.position;
        Vector3 forward = transform.forward;

        Gizmos.DrawWireSphere(currentPosition + forward * ForwardRange, Radius);
    }
}
