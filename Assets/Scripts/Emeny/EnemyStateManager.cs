using UnityEngine;
using UnityEngine.AI;
using static EnemyAI;
using static UnityEngine.Rendering.DebugUI.Table;

public class EnemyStateManager : MonoBehaviour
{
    EnemyBaseState cunrentstate;
    public IdleState idleState= new IdleState();
    public PatrolState patrolstate = new PatrolState();    
    public ChaseState chasestate = new ChaseState();   
    public AttackState attackstate = new AttackState();

    //condition for changing states
    public bool playerInRange = false;
    public float speed = 2f;

    [Header("General Settings")] 
    public float attackRange = 1.5f;
    public GameObject player;
    public Color StateColor = Color.white;

    [Header("Patrol Settings")]
    public Transform[] patrolPoints; 
    public Transform currentTarget;

    //Pathfinding
    public NavMeshAgent agent;

    //costume animation and sprite
    public SpriteRenderer spriteRenderer;
    // Attacking
    public bool isAttacking = false;

    public HealthBar healthBar;
    public int maxHealth = 20;
    private int currentHealth;
    public bool isdead = false;
    public Animator animator;

    public int Damage = 10;
    public float pushForce = 5f;
    public PlayerControllers playerControllerComponent;

    //VFX
    public GameObject attackVFXPrefab;
    public GameObject attackbulletVFXPrefab;
    //FX Sound
    public AudioClip hitsound;


    void Start()
    {
        //Change colors with hex code
        //ColorUtility.TryParseHtmlString("#FFD300", out chaseColor);
        //ColorUtility.TryParseHtmlString("#FF3744", out attackColor);
        spriteRenderer = GetComponent<SpriteRenderer>();
        player.GetComponent<PlayerControllers>();

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        //Max health at start
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        cunrentstate = patrolstate;
        patrolstate.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        cunrentstate.UpdateState(this);
    }
    public void SwitchState(EnemyBaseState state)
    {
        cunrentstate=state;
        state.EnterState(this);
    }
    // Detection
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Bullet"))
        {
            animator.SetBool("Hit", true);
            SoundFXManager.Instance.PlaySound(hitsound, transform);
            GameObject vfx = Instantiate(
            attackbulletVFXPrefab,
            other.transform.position,
            Quaternion.identity
        );
            
            Destroy(other.gameObject);
            TakeDamage(playerControllerComponent.damage);
            Destroy(vfx,0.25f);
        }

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            isdead = true;
            SwitchState(idleState);
            Die();
            
        }
    }

    public void Die()
    {
        agent.isStopped = true;

        animator.SetBool("Dead", true);
        spriteRenderer.color = Color.gray;
        Destroy(healthBar);
        Destroy(gameObject, 1f);

    }
}
