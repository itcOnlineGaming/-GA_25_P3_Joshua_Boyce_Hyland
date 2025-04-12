using Codice.CM.Common;
using System.Numerics;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SphereTraversal : Traversal
{


    public GameObject planet;
    public float speed;


    public override void MoveToTarget(GameObject t_target, Rigidbody rb)
    {
        UnityEngine.Vector3 moveDirection = getPlanetRelativeTargetDirection(t_target.transform);

        moveDirection += seperationForce();

        UnityEngine.Vector3 move = moveDirection * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);
        UnityEngine.Vector3 directionFromCenter = (rb.position - planet.transform.position).normalized;
        rb.position = planet.transform.position + directionFromCenter * UnityEngine.Vector3.Distance(rb.position, planet.transform.position);
        UnityEngine.Vector3 aimVector;

        aimVector = t_target.transform.position - transform.position; //vector to player pos
        aimVector = UnityEngine.Vector3.ProjectOnPlane(aimVector, transform.up); //project it on an x-z plane 
        UnityEngine.Quaternion newEnemyRotation = UnityEngine.Quaternion.LookRotation(aimVector, transform.up);

        transform.rotation = UnityEngine.Quaternion.Slerp(transform.rotation, newEnemyRotation, 10f * Time.deltaTime); //slowly transition
    }


    protected UnityEngine.Vector3 getPlanetRelativeTargetDirection(Transform target)
    {
        UnityEngine.Vector3 gravityDir = (transform.position - planet.transform.position).normalized;

        UnityEngine.Vector3 targetDir = (target.transform.position - transform.position).normalized;

        UnityEngine.Vector3 planetRelativePos = UnityEngine.Vector3.ProjectOnPlane(targetDir, gravityDir).normalized;

        UnityEngine.Vector3 moveDirection = UnityEngine.Vector3.Cross(gravityDir, UnityEngine.Vector3.Cross(planetRelativePos, gravityDir)).normalized;

        return moveDirection;
    }

}
