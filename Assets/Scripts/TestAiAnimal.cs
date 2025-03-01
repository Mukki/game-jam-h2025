using System;
using UnityEngine;
using UnityEngine.AI;

public class TestAiAnimal : MonoBehaviour
{
    public BoundedValues BoundedValues;

    private void FixedUpdate()
    {
        if (gameObject.TryGetComponent<NavMeshAgent>(out var agent))
        {
            agent.SetDestination(new Vector3 (0, 0, 0));
        }
    }
}
