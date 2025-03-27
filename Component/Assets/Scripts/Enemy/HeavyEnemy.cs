using UnityEngine;

public class HeavyEnemy : MeleeEnemy
{

    public static int maxDamage = 15;
    public static float maxSpeed = 1f;
    public static float maxHealth = 35f;

    private int goldGainOnDeath = 150;

    private void Start()
    {
        damage = maxDamage;
        speed = maxSpeed;
        health = maxHealth;
        KillGoldGain = goldGainOnDeath;


       // animator.SetFloat("SpeedMultiplier", maxSpeed);
    }
}
