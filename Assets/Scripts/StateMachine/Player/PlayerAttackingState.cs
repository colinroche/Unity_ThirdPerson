using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private Attack attack;

    /*private float previousFrameTime;*/
    private bool alreadyAppliedForce;

    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        attack = stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {
        stateMachine.Weapon.SetAttack(attack.Damage, attack.Knockback);
        // Transistion instead of just play.
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        FaceTarget();

        float normalizedTime = GetNormalizedTime(stateMachine.Animator, "Attack");

        if (normalizedTime < 1f) 
        {
            if (normalizedTime >= attack.ForceTime)
            {
                TryApplyForce();
            }

            if (stateMachine.InputReader.IsAttacking)
            {
                TryComboAttack(normalizedTime);
            }
        }
        else
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

        /*previousFrameTime = normalizedTime;*/
    }

    public override void Exit() { }

    private void TryComboAttack(float normalizedTime)
    {
        // Check if attack can combo
        if (attack.ComboStateIndex == -1) { return; }

        // Check if far enough through state to combo
        if (normalizedTime < attack.ComboAttackTime) { return; }

        stateMachine.SwitchState(new PlayerAttackingState(stateMachine, attack.ComboStateIndex));
    }

    private void TryApplyForce()
    {
        if (alreadyAppliedForce) { return; }

        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * attack.Force);
        alreadyAppliedForce = true;
    }
}
