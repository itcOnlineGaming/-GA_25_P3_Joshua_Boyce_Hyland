using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Analytics;



// Placeholder Enemy Class
public class Enemy : MonoBehaviour
{
  
    [HideInInspector] public int damage = 10;
    [HideInInspector] public float speed = 1.25f;
    public GameObject planet;
 

    
    //public Animator animator;
    public GameObject target;


    protected Rigidbody rb;
    [HideInInspector]  protected float health = 15f;

    public bool attacking = false;
    public bool grounded = false;
    protected int KillGoldGain;
    public AnimationManager animationManager;

    private CapsuleCollider capsuleCollider;    

    private void Awake()
    {
        rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.freezeRotation = true;
       

        capsuleCollider = animationManager.createColliderBasedOnModel();

    }

    private void Update()
    {
        if (health <= 0 && animationManager.animationStat != AnimationState.Dead)
        {
            // need to find different way as this will cause enemies to fall through planet
            //capsuleCollider.enabled = false;
            //8
            //GameManager.Instance.UpdateEnemyKilled();
            //GameManager.Instance.UpdateCoinCount(KillGoldGain);
            animationManager.animationStat = AnimationState.Dead;

        }
    }

    public virtual void attackTarget()
    {
       if (attacking && target != null)
        {
            //8
            //target.GetComponent<Tower>().TakeDamage(damage);
        }
    }

 
    protected void findClosestTarget()
    {
        EnemyTargetable closestTower = null;
        EnemyTargetable[] possibleTargets = FindObjectsOfType<EnemyTargetable>();


        float closestDistance = Mathf.Infinity; // Start with a large number

        foreach (EnemyTargetable target in possibleTargets)
        {
            if (!target.attackable)
            {
                continue;
            }

            float distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance < closestDistance)
            {
                closestTower = target;
                closestDistance = distance;
            }
        }


        if (closestTower != null)
        {
            target = closestTower.gameObject;
        }

    }

    protected void MoveToTarget()
    {

        Vector3 moveDirection = getPlanetRelativeTargetDirection(target.transform);

        moveDirection += seperationForce();

        Vector3 move = moveDirection * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);

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

    protected Vector3 getPlanetRelativeTargetDirection(Transform target)
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
            //8
           //TakeDamage(other.GetComponent<Projectile>().damage);
        }
    }


   
  
}

