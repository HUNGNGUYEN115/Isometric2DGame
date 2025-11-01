using UnityEngine;
using UnityEngine.InputSystem;
using static EnemyAI;
public class PlayerControllers : MonoBehaviour
{
    public Rigidbody2D rb;
    public float movespeed = 5f;
    public PlayerInputAction PlayerControls;
    Vector2 moveDirection= Vector2.zero;
    private InputAction move;
    private InputAction fire;
    public Animator animator;
    private SpriteRenderer spriteRenderer;

    //Health
    public HealthBar healthBar;
    public int maxHealth = 20;
    public int currentHealth;

    //Gun
    public int damage;
    public GameObject weapon;
    private void Awake()
    {
        //Max health at start
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        PlayerControls = new PlayerInputAction();
    }
    void Start()
    {

         spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        move = PlayerControls.Player.Move;
        move.Enable();

        fire = PlayerControls.Player.Fire;
        fire.Enable();
        

    }
    
    private void OnDisable()
    {
        move.Disable();
        
    }
   

    // Update is called once per frame
    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();
        // Calculate the speed value
        float speed = moveDirection.sqrMagnitude;
        animator.SetFloat("Speed",speed);
        
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDirection.x*movespeed, moveDirection.y*movespeed);
        // Rotate to face movement direction (only if moving)
        // Flip left/right
        if (moveDirection.x > 0.01f)
            spriteRenderer.flipX = false;   // face right
        else if (moveDirection.x < -0.01f)
            spriteRenderer.flipX = true;   // face left
    }
 
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {

            Die();
            
        }
    }
    public void Die()
    {
        
        animator.SetBool("Dead", true);
        Destroy(weapon);
        Destroy(gameObject, 1f);
        PlayerControls.Disable();

    }
    public void Health(int health)

    {
        if (currentHealth < maxHealth)
        {
            currentHealth += health;
            healthBar.SetHealth(currentHealth);


        }
        else if (currentHealth + health > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            Debug.Log("You are full");
        }
        
    }
}
