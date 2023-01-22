using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : IInputManager
{
    Mouse mouse = Mouse.current;
    public bool leftClick => mouse.leftButton.wasPressedThisFrame;
    public Vector3 InputPosition => mouse.position.ReadValue();
}