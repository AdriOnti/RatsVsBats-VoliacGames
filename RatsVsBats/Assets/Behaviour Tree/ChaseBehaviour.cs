using UnityEngine;
using UnityEngine.AI;


public class ChaseBehaviour : MonoBehaviour
{
    public float Speed;
    private Rigidbody _rb;

    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private Transform target;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }
    public void Chase(Transform target, Transform self)
    {
        agent.destination = target.position;
    }
    public void Run(Transform target, Transform self)
    {
        //_rb.velocity = (target.position - self.position).normalized * -Speed;
    }

    public void StopChasing()
    {
        //_rb.velocity = Vector2.zero;
    }
}
