using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    // variable for reference to the Player
    protected PlayerStateMachine stateMachine;

    // constructor for passing in reference
    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        // Adding gravity to movement
        stateMachine.Controller.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
    }

    protected void FaceTarget()
    {
        // Check that we have a target
        if (stateMachine.Targeter.CurrentTarget == null) { return; }
        
        // Take targets position and subtract our position to get a vector pointing from out player to target
        Vector3 lookPos = stateMachine.Targeter.CurrentTarget.transform.position - stateMachine.transform.position;
        lookPos.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(lookPos);
    }

    protected void ReturnToLocomotion()
    {
        if (stateMachine.Targeter.CurrentTarget != null) 
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
        else
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }
}
