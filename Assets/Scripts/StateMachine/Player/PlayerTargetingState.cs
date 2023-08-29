using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    private readonly int TargetingForwardHash = Animator.StringToHash("TargetingForward");
    private readonly int TargetingRightHash = Animator.StringToHash("TargetingRight");

    private Vector2 dogdingDirectionInput;

    private float remainingDodgeTime;

    private const float smoothTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;

    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        // When Event is triggered subscribe.
        stateMachine.InputReader.CancelEvent += OnCancel;
        stateMachine.InputReader.DodgeEvent += OnDodge;

        // Play Animation
        stateMachine.Animator.CrossFadeInFixedTime(TargetingBlendTreeHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.IsAttacking == true)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
            return;
        }

        if (stateMachine.InputReader.IsBlocking == true)
        {
            stateMachine.SwitchState(new PlayerBlockingState(stateMachine));
            return;
        }

        if (stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }

        Vector3 movement = CalculateMovement(deltaTime);

        Move(movement * stateMachine.TargetingMovementSpeed, deltaTime);

        UpdateAnimator(deltaTime);

        FaceTarget();
    }

    public override void Exit()
    {
        // When Event is triggered unsubscribe.
        stateMachine.InputReader.CancelEvent -= OnCancel;
        stateMachine.InputReader.DodgeEvent -= OnDodge;
    }

    private void OnCancel()
    {
        stateMachine.Targeter.Cancel();

        // Switch State to FreeLook State
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    private void OnDodge()
    {
        if (Time.time - stateMachine.PreviousDodgeTime < stateMachine.DodgeCooldown) { return; } 

        stateMachine.SetDodgeTime(Time.time);
        dogdingDirectionInput = stateMachine.InputReader.MovementValue;
        remainingDodgeTime = stateMachine.DodgeDuration;
    }

    private Vector3 CalculateMovement(float deltaTime)
    {
        Vector3 movement = new Vector3();

        if (remainingDodgeTime > 0f)
        {
            movement += stateMachine.transform.right * dogdingDirectionInput.x 
                            * stateMachine.DodgeLength / stateMachine.DodgeDuration;
            movement += stateMachine.transform.forward * dogdingDirectionInput.y 
                            * stateMachine.DodgeLength / stateMachine.DodgeDuration;

            remainingDodgeTime = Mathf.Max(remainingDodgeTime - deltaTime, 0f);
        }
        else
        {
            // Move around target
            movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
            movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;
        }

        return movement;
    }

    private void UpdateAnimator(float deltaTime)
    {
        if (stateMachine.InputReader.MovementValue.y == 0f)
        {
            stateMachine.Animator.SetFloat(TargetingForwardHash, 0f, smoothTime, deltaTime);
        }
        else
        {
            // If > 0f use a value of 1f otherwise use a value of -1f
            float value = stateMachine.InputReader.MovementValue.y > 0f ? 1f : -1f;
            stateMachine.Animator.SetFloat(TargetingForwardHash, value, smoothTime, deltaTime);
        }

        if (stateMachine.InputReader.MovementValue.x == 0f)
        {
            stateMachine.Animator.SetFloat(TargetingRightHash, 0f, smoothTime, deltaTime);
        }
        else
        {
            float value = stateMachine.InputReader.MovementValue.x > 0f ? 1f : -1f;
            stateMachine.Animator.SetFloat(TargetingRightHash, value, smoothTime, deltaTime);
        }
    }
}
