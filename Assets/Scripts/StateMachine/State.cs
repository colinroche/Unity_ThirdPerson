using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public abstract void Enter();
    public abstract void Tick(float deltaTime);
    public abstract void Exit();

    // Normalize as animations are different lengths
    protected float GetNormalizedTime(Animator animator, string tag)
    {
        // Get data for current and next states and set layer to 0
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        // If transitioning to an attack
        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            // How far through the state we are
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }
}
