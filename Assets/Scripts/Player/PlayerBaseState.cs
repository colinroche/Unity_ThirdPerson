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
}
