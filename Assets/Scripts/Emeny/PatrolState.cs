using UnityEngine;

public class PatrolState : EnemyBaseState
{

    public override void EnterState(EnemyStateManager Enemy) 
    {
        Debug.Log("I am pocile in the night");
        
    }

    public override void UpdateState(EnemyStateManager Enemy) 
    {
        if (Enemy.playerInRange)
        {
            Enemy.SwitchState(Enemy.chasestate);
        }
        else
        {
            //Move randomly
            Enemy.spriteRenderer.color = Color.white;
            if (Enemy.currentTarget == null && Enemy.patrolPoints.Length > 0)
                Enemy.currentTarget = Enemy.patrolPoints[Random.Range(0, Enemy.patrolPoints.Length)];
            Enemy.agent.ResetPath();
            Enemy.agent.isStopped = false;
            Enemy.agent.speed = Enemy.speed;
            Enemy.agent.SetDestination(Enemy.currentTarget.position);

            // Flip sprite based on direction of velocity
            if (Enemy.agent.velocity.x > 0.1f)
                Enemy.spriteRenderer.flipX = false;
            else if (Enemy.agent.velocity.x < -0.1f)
                Enemy.spriteRenderer.flipX = true;

            // When reached patrol point then pick a new one
            if (!Enemy.agent.pathPending && Enemy.agent.remainingDistance <= 0.2f)
            {
                Transform newTarget;
                do
                {
                    newTarget = Enemy.patrolPoints[Random.Range(0, Enemy.patrolPoints.Length)];
                } while (newTarget == Enemy.currentTarget);
                Enemy.currentTarget = newTarget;
            }
        }

    }

    public override void OnCollisionEnter(EnemyStateManager Enemy) { }
}
