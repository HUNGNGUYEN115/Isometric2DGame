using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerControllers : MonoBehaviour
{
    public Rigidbody2D rb;
    public float movespeed = 5f;
    public PlayerInputAction PlayerControls;
    Vector2 moveDirection= Vector2.zero;
    private InputAction move;
    private InputAction fire;
    public Animator animator;

    
    private void Awake()
    {
        PlayerControls = new PlayerInputAction();
    }
    private void OnEnable()
    {
        move = PlayerControls.Player.Move;
        move.Enable();

        fire = PlayerControls.Player.Fire;
        fire.Enable();
        fire.performed += Fire;

    }
    
    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
    }
    void Start()
    {
        
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
            transform.localScale = new Vector3(1, 1, 1);   // face right
        else if (moveDirection.x < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);  // face left
    }
  
    private void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire!!!!");
    }
}
