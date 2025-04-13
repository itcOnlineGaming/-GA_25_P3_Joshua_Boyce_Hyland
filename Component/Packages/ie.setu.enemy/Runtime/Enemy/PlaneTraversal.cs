using UnityEngine;
using UnityEngine.AI;

public class PlaneTraversal : Traversal
{
    private NavMeshAgent agent;
    public bool canMove = true; // External control flag

    void Start()
    {
        agent = gameObject.AddComponent<NavMeshAgent>();
        agent.updatePosition = false;
        agent.updateRotation = false;
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public override void Move(Transform target)
    {
        if (enemy == null || enemy.animationManager.animationStat != AnimationState.Walking)
            return;

        if (target == null || agent == null) return;

        agent.SetDestination(target.position);

        Vector3 step = agent.desiredVelocity.normalized * speed * Time.deltaTime;
        transform.position += step;

        // Optional: rotate manually
        if (step != Vector3.zero)
        {
            Quaternion lookRot = Quaternion.LookRotation(step);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, 10f * Time.deltaTime);
        }

        agent.nextPosition = transform.position;
    }
}
