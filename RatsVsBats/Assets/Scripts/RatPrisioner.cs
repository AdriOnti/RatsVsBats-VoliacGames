using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RatPrisioner : MonoBehaviour
{
    public bool isFree;

    public Transform target;

    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
    }

    private void Update()
    {
        // TODO: Arreglar el tema del navmesh
        if (isFree)
        {
            agent.enabled = true;
            agent.SetDestination(target.position);
            transform.LookAt(target.position);
        }
    }
}
