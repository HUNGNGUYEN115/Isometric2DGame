using UnityEngine;
using UnityEngine.InputSystem;

public class OpenUI : MonoBehaviour

{

    public bool isOpen = false;
    private InputAction Button;
   
    private PlayerInputAction playerInput;
    public GameObject inventoryPanel;
    void Start()
    {
       inventoryPanel.SetActive(false);
        isOpen = false;
    }
    private void Awake()
    {
        playerInput = new PlayerInputAction();
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnEnable()
    {
        Button = playerInput.Player.Inventory;
        Button.Enable();
        Button.performed += Toggle;
       
    }
    private void OnDisable()
    {
        Button.performed -= Toggle;
       
        Button.Disable();
       
    }

    public void Toggle(InputAction.CallbackContext context)
    {
        if (isOpen)
        {
            inventoryPanel.SetActive(false);
            isOpen = false;


        }
        else
        {
            inventoryPanel.SetActive(true);
            isOpen = true;

        }

    }
}
