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
        Debug.Log("Enter");
    }

    public override void Tick(float deltaTime)
    {
        Debug.Log("Tick");
    }

    public override void Exit()
    {
        Debug.Log("Exit");
    }
}
