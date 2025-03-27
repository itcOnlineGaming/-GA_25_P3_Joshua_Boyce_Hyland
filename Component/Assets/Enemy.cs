using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;


public enum AnimationState { Idle, Walking, Attacking, Dead }



// Placeholder Enemy Class
public class Enemy : MonoBehaviour
{
    public int damage = 10;   
    public float speed = 1.25f;
    public GameObject planet;
    public MovementStragtegy movementStragtegy;
    public string targetTag;
    public AnimationState animationStat = AnimationState.Idle;
    //public Animator animator;
    public GameObject target;


    protected Rigidbody rb;
    public static int MaxHealth = 5;
    protected float health = MaxHealth;
    public static int KillGoldGain = 100;

    protected bool attacking = false;
    protected bool grounded = false;

    CapsuleCollider capsuleCollider;    

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        capsuleCollider = gameObject.GetComponent<CapsuleCollider>();

    }

    private void Update()
    {
        if (health <= 0 && animationStat != AnimationState.Dead)
        {
            // need to find different way as this will cause enemies to fall through planet
            //capsuleCollider.enabled = false;
            //GameManager.Instance.UpdateEnemyKilled();
            //GameManager.Instance.UpdateCoinCount(KillGoldGain);
            animationStat = AnimationState.Dead;

        }
    }

    public virtual void attackTarget()
    {
       if (attacking && target != null)
        {
           
           // target.GetComponent<targetType>().TakeDamage(damage);
        }
    }

    protected void checkToFindAnotherTarget()
    {
        //if (target.GetComponent<Tower>().Attackers.Count > 5 && !target.GetComponent<Tower>().Attackers.Contains(this))
        //{
        //    Tower closestTower = null;
        //    Tower[] possibleTargets = FindObjectsOfType<Tower>();


        //    if (possibleTargets.Length > 0)
        //    {
        //        float closestDistance = Mathf.Infinity; // Start with a large number


        //        foreach (Tower target in possibleTargets)
        //        {

        //            float distance = Vector3.Distance(transform.position, target.transform.position);

        //            if ((target.GetComponent<Tower>().Attackers.Count < 5))
        //            {
        //                if (distance < closestDistance)
        //                {
        //                    closestTower = target;
        //                    closestDistance = distance;
        //                }
        //            }

        //        }
        //    }

        //    if (closestTower != null)
        //    {
        //        target = closestTower.gameObject;
        //    }
        //}
    }
    protected void findClosestTarget()
    {
        GameObject closestTower = null;
        GameObject[] possibleTargets = GameObject.FindGameObjectsWithTag(targetTag);


        if (possibleTargets.Length > 0)
        {
            float closestDistance = Mathf.Infinity; // Start with a large number


            foreach (GameObject target in possibleTargets)
            {

                float distance = Vector3.Distance(transform.position, target.transform.position);

                if (distance < closestDistance)
                {
                    closestTower = target;
                    closestDistance = distance;
                }
            }
        }

        if (closestTower != null)
        {
            target = closestTower.gameObject;
        }

    }

    protected void MoveToTarget()
    {




        Vector3 moveDirection = getSurfaceRelativeTargetDirection(target.transform);

        //moveDirection += seperationForce();
        ;
        Vector3 move = moveDirection * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movementStragtegy.moveTo(target.transform.position));

        Vector3 directionFromCenter = (rb.position - planet.transform.position).normalized;
        rb.position = planet.transform.position + directionFromCenter * Vector3.Distance(rb.position, planet.transform.position);

        Vector3 aimVector;

        aimVector = target.transform.position - transform.position; //vector to player pos
        aimVector = Vector3.ProjectOnPlane(aimVector, transform.up); //project it on an x-z plane 
        Quaternion newEnemyRotation = Quaternion.LookRotation(aimVector, transform.up);

        transform.rotation = Quaternion.Slerp(transform.rotation, newEnemyRotation, 10f * Time.deltaTime); //slowly transition
    }

    protected Vector3 seperationForce()
    {
        Vector3 separation = Vector3.zero;
        Collider[] nearbyEnemies = Physics.OverlapSphere(transform.position, 1.5f); 
        foreach (Collider col in nearbyEnemies)
        {
            if (col.CompareTag("Enemy") && col.gameObject != this.gameObject)
            {
                separation += (transform.position - col.transform.position).normalized;
            }
        }

        return separation.normalized * 0.5f;
    }

    protected Vector3 getSurfaceRelativeTargetDirection(Transform target)
    {
        Vector3 gravityDir = (transform.position - planet.transform.position).normalized;

        Vector3 targetDir = (target.transform.position - transform.position).normalized;

        Vector3 planetRelativePos = Vector3.ProjectOnPlane(targetDir, gravityDir).normalized;

        Vector3 moveDirection = Vector3.Cross(gravityDir, Vector3.Cross(planetRelativePos, gravityDir)).normalized;

        return moveDirection;
    }


    public void TakeDamage(float damageNumber) => health -= damageNumber;


    protected void OnDestroy()
    {
        attacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            //TakeDamage(other.GetComponent<Projectile>().damage);
        }
    }
}

