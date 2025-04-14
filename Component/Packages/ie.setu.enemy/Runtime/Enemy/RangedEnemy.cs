using log4net.Util;
using UnityEngine;

public class RangedEnemy : Enemy
{
    
    public string shootingPointGbName = "ShootingPoint";
    public GameObject projectile;
    [HideInInspector] public GameObject shootingPoint;


    private void Start()
    {
        shootingPoint = FindChildRecursive(gameObject.transform);
    }

    public GameObject FindChildRecursive(UnityEngine.Transform parent)
    {
        foreach (UnityEngine.Transform child in parent)
        {
            if (child.name == shootingPointGbName)
                return child.gameObject;

            GameObject result = FindChildRecursive(child);
            if (result != null)
                return result;
        }
        return null;
    }
    private void FixedUpdate()
    {
        if (animationManager.animationStat != AnimationState.Dead)
        {
            // only move if we are grounded
            if (grounded)
            {
                if (target != null)
                {
                    findClosestTarget();
                    //checkToFindAnotherTarget();


                    if (IsTargetInAttackRange())
                    {
                        attacking = true;
                        animationManager.animationStat = AnimationState.Attacking;
                    }
                    if (!attacking)
                    {
                        animationManager.animationStat = AnimationState.Walking;
                        traversal.Move(target.transform);
                    }
                    else
                    {
                        rb.linearVelocity = Vector3.zero;
                    }

                }
                else
                {
                    attacking = false;
                    animationManager.animationStat = AnimationState.Idle;
                    findClosestTarget();
                }


            }


        }
        animationManager.animator.SetInteger("State", (int)animationManager.animationStat);
    }

    public override void attackTarget()
    {
        if (attacking && target != null)
        {
            GameObject instantiatedProjectile = Instantiate(projectile,shootingPoint.transform.position,shootingPoint.transform.rotation);

            instantiatedProjectile.GetComponent<Projectile>().Launch(target.transform);


        }
    }
    public bool IsTargetInAttackRange()
    {
        if (target == null) return false;

        // Get character and target bounds
        float characterSize = gameObject.GetComponentInChildren<Renderer>().bounds.extents.magnitude;
        float targetSize = target.GetComponent<Renderer>().bounds.extents.magnitude;

        // Calculate dynamic attack range
        float attackRange = (characterSize + targetSize) * attackingRange;

        // Get distance to target
        float distance = Vector3.Distance(transform.position, target.transform.position);

        // Check if within range
        return distance <= attackRange;
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == surfaceTag)
        {
            grounded = true;
        }


    }

    private void OnCollisionExit(Collision collision)
    {


        if (collision.gameObject.GetComponent<EnemyTargetable>() != null)
        {

            attacking = false;

        }
    }
}

