using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public GameObject model;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject gb = Instantiate(model, transform);
        gb.AddComponent<MeshCollider>();
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
