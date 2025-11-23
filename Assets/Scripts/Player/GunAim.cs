using UnityEngine;

using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
    private PlayerInputAction PlayerControls;
    //Aiming
    private Vector2 mousePos;
    private Camera cam;
    public SpriteRenderer playerRenderer;
    public AudioClip bulletsound;

    //Firing
    public Transform firepoint;
    public GameObject bulletPrefab;
    private float bulletSpeed=10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
       PlayerControls= new PlayerInputAction();
    }
    void Start()
    {
        cam = Camera.main;
    }
    private void OnEnable()
    {
        PlayerControls.Player.Enable();
        PlayerControls.Player.Aim.performed += Aim;
        PlayerControls.Player.Fire.performed += Fire;

    }
    private void OnDisable()
    {
        
        PlayerControls.Player.Aim.performed -= Aim;
        PlayerControls.Player.Fire.performed -= Fire;
        PlayerControls.Player.Disable();
    }
    //Aim using the mouse potision
    void Aim(InputAction.CallbackContext context)
    {
        mousePos = context.ReadValue<Vector2>();
    }
    
    void Fire(InputAction.CallbackContext context)
    {
        GameObject bullet=Instantiate(bulletPrefab,firepoint.position,firepoint.rotation * Quaternion.Euler(0f, 0f, -90f));
        SoundFXManager.Instance.PlaySound(bulletsound, transform);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firepoint.right*bulletSpeed,ForceMode2D.Impulse);

    }
    
    private void Update()
    {
        Vector2 worldMouse = cam.ScreenToWorldPoint(mousePos);
        Vector2 direction = worldMouse - (Vector2)transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // If player is flipped (facing left)
        if (playerRenderer.flipX)
        {
            // Reverse the rotation when player facing left
            transform.rotation = Quaternion.Euler(0f, 0f, angle+360);
            
            // Flip the gun 
            Vector3 localScale = Vector3.one;
            if (angle > 90 || angle < -90)
                localScale.y = -1;      
            transform.localScale = localScale;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

            Vector3 localScale = Vector3.one;
            if (angle > 90 || angle < -90)
                localScale.y = -1;
            transform.localScale = localScale;
            
        }
    }

}
