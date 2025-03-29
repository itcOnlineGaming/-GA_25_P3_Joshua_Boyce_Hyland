using System.Text.RegularExpressions;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public GameObject model;

    //public Enemy enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created




    private void Awake()
    {

    }

    void Start()
    {
        
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public CapsuleCollider createColliderBasedOnModel()
    {
        GameObject gb = Instantiate(model, transform);
        CapsuleCollider collider = gameObject.AddComponent<CapsuleCollider>();

        Bounds bounds = gb.GetComponentInChildren<Renderer>().bounds; // Get world-space bounds

        // Set height to the largest dimension (usually Y for upright capsules)
        float height = Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z);
        float radius = Mathf.Min(bounds.size.x, bounds.size.z) / 2f; // Use smaller side

        // Update CapsuleCollider
        collider.center = gb.transform.InverseTransformPoint(bounds.center);
        collider.radius = radius;
        collider.height = height;

        return collider;
    }
}
