using UnityEngine;

public abstract class EnemyTargetable : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public bool attackable = true;
   public abstract void TakeDamage(float t_damage);
}
