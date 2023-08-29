using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgingState : PlayerBaseState
{
    private readonly int DodgeBlendTreeHash = Animator.StringToHash("DodgeBlendTree");
    private readonly int DodgeForwardHash = Animator.StringToHash("DodgeForward");
    private readonly int DodgeRightHash = Animator.StringToHash("DodgeRight");

    private Vector3 dogdingDirectionInput;

    private float remainingDodgeTime;

    private const float CrossFadeDuration = 0.1f;
    public PlayerDodgingState(PlayerStateMachine stateMachine, Vector3 dogdingDirectionInput) : base(stateMachine) 
    {
        this.dogdingDirectionInput = dogdingDirectionInput;
    }

    public override void Enter()
    {
        remainingDodgeTime = stateMachine.DodgeDuration;

        // Set blendtree parameters
        stateMachine.Animator.SetFloat(DodgeForwardHash, dogdingDirectionInput.y);
        stateMachine.Animator.SetFloat(DodgeRightHash, dogdingDirectionInput.x);

        stateMachine.Animator.CrossFadeInFixedTime(DodgeBlendTreeHash, CrossFadeDuration);

        stateMachine.Health.SetInvulnerable(true);
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = new Vector3();

        movement += stateMachine.transform.right * dogdingDirectionInput.x
                           * stateMachine.DodgeLength / stateMachine.DodgeDuration;
        movement += stateMachine.transform.forward * dogdingDirectionInput.y
                        * stateMachine.DodgeLength / stateMachine.DodgeDuration;

        Move(movement, deltaTime);

        FaceTarget();

        remainingDodgeTime -= deltaTime;

        if (remainingDodgeTime <= 0f)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
    }

    public override void Exit()
    {
        stateMachine.Health.SetInvulnerable(false);
    }
}
