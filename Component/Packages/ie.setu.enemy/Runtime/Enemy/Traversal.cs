using UnityEngine;

public abstract class Traversal : MonoBehaviour
{
    public abstract void MoveToTarget(GameObject t_target, Rigidbody rb);

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
}
