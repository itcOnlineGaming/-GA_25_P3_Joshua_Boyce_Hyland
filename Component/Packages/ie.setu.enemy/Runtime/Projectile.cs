using UnityEngine;
using static Codice.Client.Common.EventTracking.TrackFeatureUseEvent.Features.DesktopGUI.Filters;

public class Projectile : MonoBehaviour
{
    public enum ProjectileType { Straight, Arc }
    public ProjectileType projectileType = ProjectileType.Straight;

    public float speed = 10f;
    public float arcHeight = 1.5f;
    public int damage = 5;
    private Transform target;
    private Vector3 startPoint;
    private float timeToTarget;
    private float launchTime;
    private Vector3 lastPosition;

    private bool initialized = false;

    public void Launch(Transform target, float timeToReach = 1f)
    {
        this.target = target;
        this.startPoint = transform.position;
        this.timeToTarget = timeToReach;
        this.launchTime = Time.time;
        this.lastPosition = transform.position;
        initialized = true;

        if (projectileType == ProjectileType.Straight)
        {
            Vector3 dir = (target.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }

    private void Update()
    {
        if (!initialized || target == null)
        {
            Destroy(gameObject);
            return;
        }

        switch (projectileType)
        {
            case ProjectileType.Straight:
                MoveStraight();
                break;
            case ProjectileType.Arc:
                MoveArc();
                break;
        }
    }

    void MoveStraight()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void MoveArc()
    {
        float elapsed = Time.time - launchTime;
        float arcPercntageCompletion = Mathf.Clamp01(elapsed / timeToTarget);

        Vector3 targetPos = target.position;
        Vector3 arcPos = Vector3.Lerp(startPoint, targetPos, arcPercntageCompletion);
        arcPos.y += Mathf.Sin(arcPercntageCompletion * Mathf.PI) * arcHeight;

        transform.position = arcPos;

        // Calculate direction based on movement
        Vector3 movementDir = (transform.position - lastPosition).normalized;
        if (movementDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movementDir);
        }

        lastPosition = transform.position;

        if (arcPercntageCompletion >= 1f)
        {
            HitTarget();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == target)
        {
            HitTarget();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform == target)
        {
            HitTarget();
        }
    }

    void HitTarget()
    {
        EnemyTargetable targetable = target.GetComponent<EnemyTargetable>();
        if (targetable != null)
        {
            targetable.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
