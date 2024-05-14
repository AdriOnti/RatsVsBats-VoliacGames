using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RatPrisioner : MonoBehaviour
{
    public bool isFree;
    public Transform finalTarget;
    public Transform actualTarget;
    public List<Transform> pathTargets;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (isFree && agent != null)
        {
            if(pathTargets.Count > 0)
            {
                actualTarget = pathTargets[0];
            }
            else
            {
                actualTarget = finalTarget;
            }
            agent.SetDestination(actualTarget.position);
            transform.LookAt(actualTarget.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == pathTargets[0])
        {
            Debug.Log($"He encontrado {pathTargets[0].gameObject.name}");
            pathTargets.Remove(pathTargets[0]);
            transform.position = new Vector3(transform.position.x, transform.position.y + 10f, transform.position.z);
        }   
    }
}
