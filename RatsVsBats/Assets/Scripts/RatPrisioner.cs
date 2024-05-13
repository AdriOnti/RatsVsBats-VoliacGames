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
    }

    private void Update()
    {
        if (isFree && agent != null)
        {
            agent.enabled = true;
            agent.SetDestination(target.position);
            transform.LookAt(target.position);
        }
    }
}
