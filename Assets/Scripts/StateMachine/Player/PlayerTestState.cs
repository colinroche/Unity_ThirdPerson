using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestState : PlayerBaseState
{
    private float timer;
    // constructor - as it is inheriting an abstract class
    public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    // Implementing the abstract class
    public override void Enter()
    {
        //subscribing to event call OnJump
        stateMachine.InputReader.JumpEvent += OnJump;
    }

    public override void Tick(float deltaTime)
    {
        timer += deltaTime;

        Debug.Log(timer);
    }

    public override void Exit()
    {
        // unsubscribing to event call OnJump
        stateMachine.InputReader.JumpEvent -= OnJump;
    }

    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerTestState(stateMachine));
    }
}
