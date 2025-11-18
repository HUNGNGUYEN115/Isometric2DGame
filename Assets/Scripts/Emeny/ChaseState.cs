using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ChaseState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager Enemy) 
    {

        Debug.Log("Chasing Chasing boy");
         
        ColorUtility.TryParseHtmlString("#FFD300", out Enemy.StateColor);
    }

    public override void UpdateState(EnemyStateManager Enemy) 
    {
        if (Enemy.playerInRange)
        {
            Enemy.spriteRenderer.color = Enemy.StateColor;
            if (Enemy.player == null) return;

            Enemy.agent.isStopped = false;
            //Speed up 2.0
            Enemy.agent.speed = Enemy.speed * 2.0f;
            Enemy.agent.SetDestination(Enemy.player.transform.position);

            // Flip based on movement direction
            if (Enemy.agent.velocity.x > 0.1f)
                Enemy.spriteRenderer.flipX = false;
            else if (Enemy.agent.velocity.x < -0.1f)
                Enemy.spriteRenderer.flipX = true;
            float distanceToPlayer = Vector2.Distance(Enemy.transform.position, Enemy.player.transform.position);
            if (distanceToPlayer < Enemy.attackRange)
            {
                Enemy.SwitchState(Enemy.attackstate);
            }
        }
        else
        {
            Enemy.SwitchState(Enemy.patrolstate);
        }

    }

    public override void OnCollisionEnter(EnemyStateManager Enemy) { }
}
