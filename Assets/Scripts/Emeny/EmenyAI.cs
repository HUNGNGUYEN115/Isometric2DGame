using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using JetBrains.Annotations;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState { Idle, Patrol, Chase, Attack }
    private EnemyState currentState;

    [Header("General Settings")]
    public float speed = 2f;
    public float attackRange = 1.5f;
    public Transform player;

    [Header("Patrol Settings")]
    public Transform[] patrolPoints; 
    private Transform currentTarget;

    //change color in different states
    private Color patrolColor = Color.white;
    private Color chaseColor;
    private Color attackColor;

    //condition for changing states
    public bool playerInRange = false;

    //costume animation and sprite
    private SpriteRenderer spriteRenderer;
    public Animator animator;
    //Pathfinding
    NavMeshAgent agent;
    public PlayerControllers playerControllers;
    private bool isAttacking = false;
    //Health
    public HealthBar healthBar;
    public int maxHealth = 20;
    private int currentHealth;
    public bool isdead=false;
    public int Damage = 10;
    public float pushForce = 5f;
   
    private void Start()
    {
        //Change colors with hex code
        ColorUtility.TryParseHtmlString("#FFD300", out chaseColor);
        ColorUtility.TryParseHtmlString("#FF3744", out attackColor);

        //Starting state is Patrol
        currentState = EnemyState.Patrol;
       
        spriteRenderer = GetComponent<SpriteRenderer>();

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation= false;
        agent.updateUpAxis = false;

        //Max health at start
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
       


    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                if (playerInRange)
                    
                    ChangeState(EnemyState.Chase);
                break;

            case EnemyState.Patrol:
                spriteRenderer.color = patrolColor;
                Patrol();
                if (playerInRange)
                    ChangeState(EnemyState.Chase);
                break;

            case EnemyState.Chase:
                spriteRenderer.color= chaseColor;
                Chase();
                float distanceToPlayer = Vector2.Distance(transform.position, player.position);
                if (distanceToPlayer < attackRange)
                    ChangeState(EnemyState.Attack);
                else if (!playerInRange)
                    ChangeState(EnemyState.Patrol);
                break;

            case EnemyState.Attack:
                spriteRenderer.color = attackColor; 
                Attack();
                if (!playerInRange)
                    ChangeState(EnemyState.Patrol);
                else if (Vector2.Distance(transform.position, player.position) > attackRange)
                    ChangeState(EnemyState.Chase);
                break;
        }
    }

    private void ChangeState(EnemyState newState)
    {
        currentState = newState;
    }

    private void Patrol()
    {
        if (currentTarget == null && patrolPoints.Length > 0)
            currentTarget = patrolPoints[Random.Range(0, patrolPoints.Length)];

        agent.isStopped = false;
        agent.speed = speed;
        agent.SetDestination(currentTarget.position);

        // Flip sprite based on direction of velocity
        if (agent.velocity.x > 0.1f)
            spriteRenderer.flipX = false;
        else if (agent.velocity.x < -0.1f)
            spriteRenderer.flipX = true;

        // When reached patrol point → pick a new one
        if (!agent.pathPending && agent.remainingDistance <= 0.2f)
        {
            Transform newTarget;
            do
            {
                newTarget = patrolPoints[Random.Range(0, patrolPoints.Length)];
            } while (newTarget == currentTarget);
            currentTarget = newTarget;
        }
    }


    private void Chase()
    {
        if (player == null) return;

        agent.isStopped = false;
        //Speed up 2.0
        agent.speed = speed * 2.0f;
        agent.SetDestination(player.position);

        // Flip based on movement direction
        if (agent.velocity.x > 0.1f)
            spriteRenderer.flipX = false;
        else if (agent.velocity.x < -0.1f)
            spriteRenderer.flipX = true;
    }


    private void Attack()
    {
        if (isAttacking) return;
        isAttacking = true;
        agent.isStopped = true; // Stop moving while attacking
        
        playerControllers.TakeDamage(Damage);
        
      
        StartCoroutine(Afterattack());


    }
    IEnumerator Afterattack()
    {
        

        yield return new WaitForSeconds(2f);
        isAttacking=false;
        ChangeState(EnemyState.Idle);
    }


    // Detection
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Bullet"))
        {
            animator.SetBool("Hit",true);
            Destroy(other.gameObject);
            TakeDamage(playerControllers.damage)  ;
        }

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth<=0)
        {

            Die();
            isdead = true;
        }
    }

    public void Die()
    {
        ChangeState(EnemyState.Idle);
        agent.isStopped = true;
        
        animator.SetBool("Dead", true);
        spriteRenderer.color = Color.gray;
        Destroy(gameObject, 1f);

    }


}
