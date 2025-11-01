using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static PlayerInputAction input; // Shared instance

    private void Awake()
    {
        if (input == null)
        {
            input = new PlayerInputAction();
            input.Enable();
        }
    }
}
