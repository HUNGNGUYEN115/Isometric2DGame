using UnityEngine;
using System.Collections;
using static EnemyAI;

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


        Enemy.StartCoroutine(Afterattack(Enemy));
    }
    IEnumerator Afterattack(EnemyStateManager Enemy)
    {


        yield return new WaitForSeconds(1f);
        Enemy.isAttacking = false;
        Enemy.SwitchState(Enemy.idleState);
    }

    public override void OnCollisionEnter(EnemyStateManager Enemy) { }
}
