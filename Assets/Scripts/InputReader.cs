using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// can only inherit 1 Class, anything after is an Interface
public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    // using events to link to State
    public event Action JumpEvent;

    private Controls controls;
    private void Start()
    {
        // make an instance of Controls
        controls = new Controls();
        // link to Controls class
        controls.Player.SetCallbacks(this);

        controls.Player.Enable();
    }

    private void OnDestroy()
    {
        controls.Player.Disable();
    }

    // implement Interface
    public void OnJump(InputAction.CallbackContext context)
    {
        // only care if the button is pressed not released
        if (!context.performed) { return; }

        // invoke jump event for whoever is listen
        JumpEvent?.Invoke();
    }
}
