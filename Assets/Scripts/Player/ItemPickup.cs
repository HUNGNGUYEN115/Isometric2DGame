using UnityEngine;
using UnityEngine.InputSystem;

public class ItemPickup : MonoBehaviour
{
    public Items itemData; 
    private bool playerInRange = false;
    public InventorySystem playerInventory;

    private PlayerInputAction playerInput;
    private InputAction interactAction;
    

    private void Awake()
    {
        // Create input system and bind Interact action
        playerInput = new PlayerInputAction();
    }

    private void OnEnable()
    {
        interactAction = playerInput.Player.Pickup;
        interactAction.Enable();
        interactAction.performed += Pickup;
    }

    private void OnDisable()
    {
        interactAction.performed -= Pickup;
        interactAction.Disable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            playerInRange = true;
            playerInventory = other.GetComponent<InventorySystem>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            playerInRange = false;
           
        }
    }

    private void Pickup(InputAction.CallbackContext context)
    {
        if (playerInRange && playerInventory)
        {
            //SoundFXManager.Instance.PlaySound(pickupsound, transform);
            playerInventory.AddItem(itemData);
            
            Destroy(gameObject);
        }
    }
}

