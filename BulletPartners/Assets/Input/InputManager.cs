using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine;
using System.Linq;
using static UnityEditor.FilePathAttribute;

public class InputManager : MonoBehaviour
{
    public static Vector2 Movement;

    private InputHandler handler;
    private Gun gun;

    private PlayerInput _playerInput;
    private InputAction _moveAction;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        var movers = FindObjectsOfType<InputHandler>();
        var index = _playerInput.playerIndex;

        handler = movers.FirstOrDefault(m => m.GetPlayerIndex() == index);
    }

    public void OnMove(CallbackContext context)
    {
        handler.SetInputVector(context.ReadValue<Vector2>());
    }

    public void OnRotate(CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();

        if (context.control.device.ToString().Contains("Mouse"))
        {
            handler.hasMouse = true;
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(inputVector), out hit, Mathf.Infinity))
            {
                handler.mouseHit = hit;
            }
        }
        else
        {
            handler.hasMouse = false;
            handler.SetRotateVector(inputVector);
        }

    }

    public void OnShoot(CallbackContext context)
    {
        if (context.started)
        {
            handler.shooting = true;
        }
        else if (context.canceled)
        {
            handler.shooting = false;
        }
    }

    public void OnSwitchWeapon(CallbackContext context)
    {
        if (context.started)
        {
            handler.SwitchGun();
        }
    }
}
