using UnityEngine;

public class AnimationScriptExample : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public Enemy enemy;


    private void Start()
    {
        
        AnimationScriptExample example = enemy.animationManager.instantiatedModel.AddComponent<AnimationScriptExample>();

        example.enabled = true;
        example.enemy = enemy;  
    }
    void attack()
    {
        enemy.attackTarget();
    }


    void death()
    {
        Destroy(enemy.gameObject);
    }
}
