using UnityEngine;

public class mediumEnemy : MeleeEnemy
{

    public static int maxDamage = 10;
    public static float maxSpeed = 5f;
    public static float maxHealth = 50f;
    private int goldGainOnDeath = 25;

    private void Start()
    {
        damage = maxDamage;
        speed = maxSpeed;
        health = maxHealth;
        KillGoldGain = goldGainOnDeath;

        animationManager.animator.SetFloat("SpeedMultiplier", maxSpeed);
    }
}
