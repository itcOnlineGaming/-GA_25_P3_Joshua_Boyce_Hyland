using Codice.CM.Common;
using System.Numerics;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SphereTraversal : Traversal
{


    public GameObject planet;
    [HideInInspector] public Rigidbody rb;
  


    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
       
    }

    public override void Move(Transform target)
    {
        if (!target || !planet || !rb) return;

        UnityEngine.Vector3 moveDirection = GetPlanetRelativeTargetDirection(target);
        moveDirection += SeparationForce();

        UnityEngine.Vector3 move = moveDirection * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);

        UnityEngine.Vector3 directionFromCenter = (rb.position - planet.transform.position).normalized;
        rb.position = planet.transform.position + directionFromCenter * UnityEngine.Vector3.Distance(rb.position, planet.transform.position);

        UnityEngine.Vector3 aimVector = target.position - rb.position;
        aimVector = UnityEngine.Vector3.ProjectOnPlane(aimVector, transform.up);
        UnityEngine.Quaternion newRot = UnityEngine.Quaternion.LookRotation(aimVector, transform.up);
        transform.rotation = UnityEngine.Quaternion.Slerp(transform.rotation, newRot, 10f * Time.deltaTime);
    }

    private UnityEngine.Vector3 GetPlanetRelativeTargetDirection(Transform target)
    {
        UnityEngine.Vector3 gravityDir = (transform.position - planet.transform.position).normalized;
        UnityEngine.Vector3 targetDir = (target.position - transform.position).normalized;
        UnityEngine.Vector3 planetRelativePos = UnityEngine.Vector3.ProjectOnPlane(targetDir, gravityDir).normalized;
        return UnityEngine.Vector3.Cross(gravityDir, UnityEngine.Vector3.Cross(planetRelativePos, gravityDir)).normalized;
    }

    private UnityEngine.Vector3 SeparationForce()
    {
        UnityEngine.Vector3 separation = UnityEngine.Vector3.zero;
        Collider[] nearby = Physics.OverlapSphere(transform.position, 1.5f);
        foreach (Collider col in nearby)
        {
            if (col.CompareTag("Enemy") && col.gameObject != gameObject)
            {
                separation += (transform.position - col.transform.position).normalized;
            }
        }
        return separation.normalized * 0.5f;
    }

}
