using UnityEngine;
using UnityEngine.InputSystem;

public class OpenUI : MonoBehaviour

{

    public bool isOpen = false;
    private InputAction Button;
   
    public PlayerInputAction PlayerControls;
    public GameObject inventoryPanel;
    void Start()
    {
        isOpen = false;
        inventoryPanel.SetActive(isOpen);
        
    }
    private void Awake()
    {
        PlayerControls = new PlayerInputAction();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        Button=PlayerControls.UI.Inventory;
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
            Time.timeScale = 1f;
            inventoryPanel.SetActive(!isOpen);
            isOpen = false;
           
             


        }
        else
        {
            inventoryPanel.SetActive(!isOpen);
            isOpen = true;
            Time.timeScale = 0f;
            

        }

    }
    public void ToggleClick()
    {
        if (isOpen)
        {
            Time.timeScale = 1f;
            inventoryPanel.SetActive(!isOpen);
            isOpen = false;
            


        }
        else
        {
            inventoryPanel.SetActive(!isOpen);
            isOpen = true;
            Time.timeScale = 0f;
            

        }
    }
}
