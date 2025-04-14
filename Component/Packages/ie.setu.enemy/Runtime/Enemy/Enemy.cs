using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Analytics;



// Placeholder Enemy Class
public class Enemy : MonoBehaviour
{

    public float attackingRange = 0.8f;
    public int damage = 10;
    public float speed = 1.25f;
    
    public float animationSpeedScaler = 1f;
    public GameObject planet;


    public Traversal traversal;

    //public Animator animator;
    [HideInInspector] public GameObject target;


    protected Rigidbody rb;
    [HideInInspector]  protected float health = 15f;
    [HideInInspector] public bool attacking = false;
    [HideInInspector] public bool grounded = false;
    protected int KillGoldGain;
    public AnimationManager animationManager;

    private CapsuleCollider capsuleCollider;    

    private void Awake()
    {
        rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.freezeRotation = true;
       

      

        capsuleCollider = animationManager.createColliderBasedOnModel();

        animationManager.animator.SetFloat("SpeedMultiplier", speed * animationSpeedScaler);
        traversal.speed = speed;

    }

    private void Update()
    {
        if (health <= 0 && animationManager.animationStat != AnimationState.Dead)
        {
      
            animationManager.animationStat = AnimationState.Dead;

        }
    }

    public virtual void attackTarget()
    {
        if (attacking && target != null)
        {
            target.GetComponent<EnemyTargetable>().TakeDamage(damage);
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



   
  
}

