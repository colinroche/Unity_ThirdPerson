using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        // When Cancel Event is triggered subscribe.
        stateMachine.InputReader.CancelEvent += OnCancel;
    }

    public override void Tick(float deltaTime)
    {

    }

    public override void Exit()
    {
        // When Cancel Event is triggered unsubscribe.
        stateMachine.InputReader.CancelEvent -= OnCancel;
    }

    private void OnCancel()
    {
        // Switch State to FreeLook State
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }
}
