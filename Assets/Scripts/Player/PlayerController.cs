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

    //Knockback
    private Vector2 knockback;
    private float knockbackDecay = 10f;

    //FXSound
    public AudioClip hitSound;
    public AudioClip healthSound;
    public AudioClip deathSound;

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
        Vector2 finalVelocity = new Vector2(moveDirection.x * movespeed, moveDirection.y * movespeed);
        // Add knockback
        finalVelocity += knockback;
        rb.linearVelocity = finalVelocity;
        knockback = Vector2.Lerp(knockback, Vector2.zero, knockbackDecay * Time.fixedDeltaTime);

        // Rotate to face movement direction (only if moving)
        // Flip left/right
        if (moveDirection.x > 0.01f)
            spriteRenderer.flipX = false;   // face right
        else if (moveDirection.x < -0.01f)
            spriteRenderer.flipX = true;   // face left

    }
    public void ApplyKnockback(Vector2 force)
    {
        knockback = force;
        Debug.Log("Knock Knock");
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        SoundFXManager.Instance.PlaySound(hitSound, transform);
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
        SoundFXManager.Instance.PlaySound(deathSound, transform);
        GameManager.Instance.LoseGame();

    }
    public void Health(int health)

    {
        SoundFXManager.Instance.PlaySound(healthSound, transform);
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
