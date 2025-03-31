using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;


public enum AnimationState { Idle, Walking, Attacking, Dead }

public class AnimationManager : MonoBehaviour
{
    public GameObject model;
    public RuntimeAnimatorController controller;

    [HideInInspector] public GameObject instantiatedModel;
    [HideInInspector] public Animator animator;
    [HideInInspector] public AnimationState animationStat = AnimationState.Idle;

   
    public CapsuleCollider createColliderBasedOnModel()
    {
        instantiatedModel = Instantiate(model, transform);
        CapsuleCollider collider = gameObject.AddComponent<CapsuleCollider>();

        Bounds bounds = instantiatedModel.GetComponentInChildren<Renderer>().bounds; // Get world-space bounds

        // Set height to the largest dimension (usually Y for upright capsules)
        float height = Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z);
        float radius = Mathf.Min(bounds.size.x, bounds.size.z) / 2f; // Use smaller side

        // Update CapsuleCollider
        collider.center = instantiatedModel.transform.InverseTransformPoint(bounds.center);
        collider.radius = radius;
        collider.height = height;


        animator = instantiatedModel.AddComponent<Animator>();
        animator.enabled = true;
        animator.runtimeAnimatorController = controller;

        return collider;
    }
}
