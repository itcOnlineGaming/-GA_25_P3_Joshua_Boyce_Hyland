using Unity.AI.Navigation;
using UnityEngine;

public class testing : MonoBehaviour
{
  
    public Vector3 planeScale = new Vector3(10, 1, 10);
    private GameObject plane;
    private float halfPlaneWidth;

    public GameObject model;
    public RuntimeAnimatorController controller;

    void Start()
    {
        CreatePlaneWithNavMesh();
        SpawnCubeOnLeft();
        SpawnEmptyOnRight();
    }

    void CreatePlaneWithNavMesh()
    {
        plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.position = Vector3.zero;
        plane.transform.localScale = planeScale;
        plane.name = "NavMeshPlane";

        NavMeshSurface surface = plane.AddComponent<NavMeshSurface>();
        surface.BuildNavMesh();

        halfPlaneWidth = 5f * plane.transform.localScale.x;

    }

    void SpawnCubeOnLeft()
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(-halfPlaneWidth, 0.5f, 0f);
        cube.name = "Target";

        // Attach your custom script
        ExampleEnemy enemy = cube.AddComponent<ExampleEnemy>();
        enemy.health = 100;



 
    }

    void SpawnEmptyOnRight()
    {
        GameObject empty = new GameObject("Enemy");

        // prevents from gamopbject awake and start being called, causign error with components not beinh present
        empty.SetActive(false); 


        empty.transform.position = new Vector3(halfPlaneWidth, 0f, 0f);

        MeleeEnemy meleeEnemy = empty.AddComponent<MeleeEnemy>();
        AnimationManager animationManager = empty.AddComponent<AnimationManager>();
        AnimationScriptExample animationScript = empty.AddComponent<AnimationScriptExample>();
        PlaneTraversal planeTraversal = empty.AddComponent<PlaneTraversal>();

        meleeEnemy.animationManager = animationManager;
        meleeEnemy.traversal = planeTraversal;
        meleeEnemy.speed = 5;
        meleeEnemy.animationSpeedScaler = 0.5f;

        animationManager.model = model;         
        animationManager.controller = controller; 

        animationScript.enemy = meleeEnemy;
        planeTraversal.enemy = meleeEnemy;

        empty.SetActive(true);
    }

}
