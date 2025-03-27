using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SphereStrategy :MovementStragtegy
{

    public string SphereName;
    private GameObject sphere;
 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sphere = GameObject.Find(SphereName);
    }

    // Update is called once per frame
    public override Vector3 moveTo(Vector3 position)
    {
        Vector3 moveDirection = getSurfaceRelativeTargetDirection(position);
        Vector3 directionFromCenter = (gameObject.transform.position - sphere.transform.position).normalized;


        return sphere.transform.position + directionFromCenter * Vector3.Distance(gameObject.transform.position, sphere.transform.position); ;
    }

    private Vector3 getSurfaceRelativeTargetDirection(Vector3 target)
    {
        Vector3 gravityDir = (transform.position - sphere.transform.position).normalized;

        Vector3 targetDir = (target - transform.position).normalized;

        Vector3 planetRelativePos = Vector3.ProjectOnPlane(targetDir, gravityDir).normalized;

        Vector3 moveDirection = Vector3.Cross(gravityDir, Vector3.Cross(planetRelativePos, gravityDir)).normalized;

        return moveDirection;
    }
}
