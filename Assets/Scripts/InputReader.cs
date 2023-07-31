using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// can only inherit 1 Class, anything after is an Interface
public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public bool IsAttacking { get; private set; }
    public Vector2 MovementValue {  get; private set; }

    // using events to link to State
    public event Action JumpEvent;
    public event Action DodgeEvent;
    public event Action TargetEvent;
    public event Action CancelEvent;

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

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        DodgeEvent?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // takes in value of Move input
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
       
    }

    public void OnTarget(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        TargetEvent?.Invoke();
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        CancelEvent?.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext context)
    { 
        if (context.performed)
        {
            IsAttacking = true;
        }
        else if (context.canceled)
        {
            IsAttacking = false;
        }
    }
}
