using UnityEngine;
using System.Collections;
using static EnemyAI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AttackState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager Enemy) 
    {
        Debug.Log(" I bite you");
        Enemy.playerControllerComponent= Enemy.player.GetComponent<PlayerControllers>();
        ColorUtility.TryParseHtmlString("#FF3744", out Enemy.StateColor);
    }

    public override void UpdateState(EnemyStateManager Enemy) 
    {
        if (Enemy.isAttacking) return;
        Enemy.isAttacking = true;
        Enemy.spriteRenderer.color=Enemy.StateColor;
        Enemy.agent.isStopped = true; // Stop moving while attacking
        
        Enemy.playerControllerComponent.TakeDamage(Enemy.Damage);
        SpawnAttackVFX(Enemy);
        Rigidbody2D rb;
        rb=Enemy.player.GetComponent<Rigidbody2D>();
        Vector2 direction=(Enemy.player.transform.position-Enemy.transform.position).normalized;
        Enemy.playerControllerComponent.ApplyKnockback(direction * Enemy.pushForce);
        Enemy.StartCoroutine(Afterattack(Enemy));
    }
    IEnumerator Afterattack(EnemyStateManager Enemy)
    {


        yield return new WaitForSeconds(1f);
        Enemy.isAttacking = false;
        Enemy.SwitchState(Enemy.idleState);
    }
    private void SpawnAttackVFX(EnemyStateManager Enemy)
    {
        if (Enemy.attackVFXPrefab == null) return;

        // Direction from enemy to player
        Vector2 dir = (Enemy.player.transform.position - Enemy.transform.position).normalized;


        Quaternion rot = Enemy.transform.rotation * Quaternion.Euler(0, 0, 60f);
        // Instantiate VFX
        GameObject vfx = GameObject.Instantiate(
            Enemy.attackVFXPrefab,
            Enemy.player.transform.position,
            rot
        );
        
        SpriteRenderer sprite = vfx.GetComponent<SpriteRenderer>();
        if (dir.x > 0)
        {
            sprite.flipY = true;
            Debug.Log("FlipY");
        }
              
        else
        {
            sprite.flipY = false;
            Debug.Log("FlipX");
        }
            

        // Destroy after animation finishes
        Animator anim = vfx.GetComponent<Animator>();
        if (anim != null)
        {
            float duration = anim.GetCurrentAnimatorStateInfo(0).length;
            GameObject.Destroy(vfx, duration);
        }
        else
        {
            GameObject.Destroy(vfx, 1f);
        }
    }


    public override void OnCollisionEnter(EnemyStateManager Enemy) { }
}
