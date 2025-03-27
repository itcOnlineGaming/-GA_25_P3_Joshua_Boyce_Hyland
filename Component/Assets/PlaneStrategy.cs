using UnityEngine;
using UnityEngine.AI;

public class PlaneStrategy : MovementStragtegy
{
    public NavMeshAgent Agent;
    public GameObject parent;
    private Rigidbody rb;

    private void Start()
    {
        
        Agent = parent.AddComponent<NavMeshAgent>();   
    }
    public override Vector3 moveTo(Vector3 position)
    {
        
        Agent.SetDestination(position);

        Vector3 direction = Agent.desiredVelocity.normalized;
    

        return direction;
    }
}
