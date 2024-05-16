using UnityEngine;
using UnityEngine.AI;

public class RatPrisioner : MonoBehaviour
{
    public bool isFree;
    public bool targetArrived;
    public Transform finalTarget;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (isFree && agent != null)
        {
            agent.SetDestination(finalTarget.position);
            transform.LookAt(finalTarget.position);
        }

        if (targetArrived) finalTarget = null;
    }
}
