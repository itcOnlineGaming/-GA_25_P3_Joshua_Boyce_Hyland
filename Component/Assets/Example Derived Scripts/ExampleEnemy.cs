using UnityEngine;

public class ExampleEnemy : EnemyTargetable
{

    

    public override void TakeDamage(float t_damage)
    {
        health -= t_damage; ;
    }

    private void Update()
    {
        if( health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
