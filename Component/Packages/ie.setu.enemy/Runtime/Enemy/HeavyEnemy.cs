using UnityEngine;

public class HeavyEnemy : MeleeEnemy
{

    public int maxDamage = 15;
    public float maxSpeed = 5f;
    public float maxHealth = 35f;
    public float animationSpeedScaler = 1f;

    private int goldGainOnDeath = 150;

    private void Start()
    {
        damage = maxDamage;
        speed = maxSpeed;
        health = maxHealth;
        KillGoldGain = goldGainOnDeath;

        animationManager.animator.SetFloat("SpeedMultiplier", maxSpeed * animationSpeedScaler);

    }
}
