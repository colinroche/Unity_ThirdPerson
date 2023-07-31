using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private Attack attack;

    private float previousFrameTime;

    private string attackTag = "Attack";

    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        attack = stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {
        // Transistion instead of just play.
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        FaceTarget();

        float normalizedTime = GetNormalizedTime();

        // Check if we are still inside of the previous state
        if (normalizedTime > previousFrameTime && normalizedTime < 1f) 
        {
            if (stateMachine.InputReader.IsAttacking)
            {
                TryComboAttack(normalizedTime);
            }
        }
        else
        {
            // Go back to locomotion
        }

        previousFrameTime = normalizedTime;
    }

    public override void Exit()
    {
        
    }

    private void TryComboAttack(float normalizedTime)
    {
        // Check if attack can combo
        if (attack.ComboStateIndex == -1) { return; }

        // Check if far enough through state to combo
        if (normalizedTime < attack.ComboAttackTime) { return; }

        stateMachine.SwitchState(new PlayerAttackingState(stateMachine, attack.ComboStateIndex));
    }

    // Normalize as animations are different lengths
    private float GetNormalizedTime()
    {
        // Get data for current and next states and set layer to 0
        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = stateMachine.Animator.GetNextAnimatorStateInfo(0);

        // If transitioning to an attack
        if (stateMachine.Animator.IsInTransition(0) && nextInfo.IsTag(attackTag))
        {
            // How far through the state we are
            return nextInfo.normalizedTime;
        }
        else if (!stateMachine.Animator.IsInTransition(0) && currentInfo.IsTag(attackTag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }
}
