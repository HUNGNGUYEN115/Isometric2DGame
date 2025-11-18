using UnityEngine;

public class IdleState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager Enemy)
    {
        Debug.Log("I stand in the air");
    }

    public override void UpdateState(EnemyStateManager Enemy) 
    {
        if (!Enemy.isdead)
        {


            if (Enemy.playerInRange)
            {
                Enemy.SwitchState(Enemy.chasestate);
            }
            else
            {
                Enemy.SwitchState(Enemy.patrolstate);
            }
        }

    }

    public override void OnCollisionEnter(EnemyStateManager Enemy){}
}
