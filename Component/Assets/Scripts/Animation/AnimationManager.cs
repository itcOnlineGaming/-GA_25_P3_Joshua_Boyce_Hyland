using System.Text.RegularExpressions;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public GameObject model;
    public CapsuleCollider collider;

    public Enemy enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    private void Awake()
    {
        GameObject gb = Instantiate(model, transform);
        collider = gb.AddComponent<CapsuleCollider>();
        //collider.convex = true;
        Rigidbody rb = gb.AddComponent<Rigidbody>();
        rb.useGravity = false;

        Bounds bounds = gb.GetComponentInChildren<Renderer>().bounds; // Get world-space bounds

        // Set height to the largest dimension (usually Y for upright capsules)
        float height = Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z);
        float radius = Mathf.Min(bounds.size.x, bounds.size.z) / 2f; // Use smaller side

        // Update CapsuleCollider
        collider.center = gb.transform.InverseTransformPoint(bounds.center);
        collider.radius = radius;
        collider.height = height;
    }

    void Start()
    {
        
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Atmosphere>() != null)
        {
            enemy.grounded = true;
        }

        if (collision.gameObject.GetComponent<EnemyTargetable>() != null)
        {
            enemy.attacking = true;
            enemy.animationStat = AnimationState.Attacking;

            collision.gameObject.GetComponent<EnemyTargetable>().TakeDamage(10);
        }

    }
}
