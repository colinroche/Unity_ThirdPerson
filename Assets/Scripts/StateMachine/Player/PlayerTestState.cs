using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestState : PlayerBaseState
{
    private float timer = 5f;
    // constructor - as it is inheriting an abstract class
    public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    // Implementing the abstract class
    public override void Enter()
    {
        Debug.Log("Enter");
    }

    public override void Tick(float deltaTime)
    {
        // decrementing timer
        timer -= deltaTime;

        Debug.Log(timer);

        // switching to new state every 5 seconds
        if (timer <= 0f)
        {
            stateMachine.SwitchState(new PlayerTestState(stateMachine));
        } 
    }

    public override void Exit()
    {
        Debug.Log("Exit");
    }
}
