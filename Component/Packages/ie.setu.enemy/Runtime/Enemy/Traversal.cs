using UnityEngine;

public abstract class Traversal : MonoBehaviour
{
    public float speed;
    public Enemy enemy;
    public abstract void Move(Transform target);
}
