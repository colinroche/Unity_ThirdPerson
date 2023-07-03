using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestState : PlayerBaseState
{
    // constructor - as it is inheriting an abstract class
    public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    // Implementing the abstract class
    public override void Enter()
    {

    }

    public override void Tick(float deltaTime)
    {
        // moving in 3D space
        Vector3 movement = new Vector3();
        // setting movement
        movement.x = stateMachine.InputReader.MovementValue.x;
        movement.y = 0;
        movement.z = stateMachine.InputReader.MovementValue.y;
        // moving player in world - move the same regardless of frame rate
        stateMachine.Controller.Move(movement * stateMachine.FreeLookMovementSpeed * deltaTime);

        // rotate player when moving left and right
        // set animations for idle and running
        if (stateMachine.InputReader.MovementValue == Vector2.zero) 
        {
            stateMachine.Animator.SetFloat("FreeLookSpeed", 0, 0.1f, deltaTime);
            return; 
        }

        stateMachine.Animator.SetFloat("FreeLookSpeed", 1, 0.1f, deltaTime);
        stateMachine.transform.rotation = Quaternion.LookRotation(movement);
    }

    public override void Exit()
    {

    }
}
