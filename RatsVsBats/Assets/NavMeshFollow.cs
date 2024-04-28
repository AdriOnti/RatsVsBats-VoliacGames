using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshFollow : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private Transform target;



    private void Start()
    {

        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        agent.destination = target.position;
    }
}
