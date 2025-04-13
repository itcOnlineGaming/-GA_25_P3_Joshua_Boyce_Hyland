using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;


public enum AnimationState { Idle, Walking, Attacking, Dead }

public class AnimationManager : MonoBehaviour
{
    public GameObject model;
    public float capsuleOffset = 0.1f;
    public RuntimeAnimatorController controller;
  

    [HideInInspector] public GameObject instantiatedModel;
    [HideInInspector] public Animator animator;
    [HideInInspector] public AnimationState animationStat = AnimationState.Idle;

   
    public CapsuleCollider createColliderBasedOnModel()
    {
        instantiatedModel = Instantiate(model, transform);
        CapsuleCollider collider = gameObject.AddComponent<CapsuleCollider>();


        Bounds bounds = instantiatedModel.GetComponentInChildren<Renderer>().bounds;

  
        float height = bounds.size.y;
        float radius = Mathf.Min(bounds.size.x, bounds.size.z) / 2f;


        float bottomY = bounds.min.y;

        
        Vector3 worldCenter = new Vector3(bounds.center.x, bottomY + height / 2f, bounds.center.z);
        Vector3 localCenter = instantiatedModel.transform.InverseTransformPoint(worldCenter);

       
        localCenter.y += capsuleOffset; 

        collider.center = localCenter;
        collider.radius = radius;
        collider.height = height;

        animator = instantiatedModel.AddComponent<Animator>();
        animator.enabled = true;
        animator.runtimeAnimatorController = controller;



        return collider;
    }
}
