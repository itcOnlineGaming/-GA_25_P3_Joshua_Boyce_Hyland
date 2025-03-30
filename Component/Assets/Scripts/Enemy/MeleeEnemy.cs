using Unity.VisualScripting;
using UnityEngine;

public class MeleeEnemy : Enemy
{
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

                    if (!attacking)
                    {
                        animationManager.animationStat = AnimationState.Walking;
                        MoveToTarget();
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

            target.GetComponent<EnemyTargetable>().TakeDamage(damage);
        }
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Atmosphere>() != null)
        {
            grounded = true;
        }

        if (collision.gameObject.GetComponent<EnemyTargetable>() != null)
        {
            attacking = true;
            animationManager.animationStat = AnimationState.Attacking;

            collision.gameObject.GetComponent<EnemyTargetable>().TakeDamage(10);
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<Atmosphere>() != null)
        {
            grounded = true;
        }

        if (collision.gameObject.GetComponent<EnemyTargetable>() != null)
        {

            attacking = false;

        }
    }
}
