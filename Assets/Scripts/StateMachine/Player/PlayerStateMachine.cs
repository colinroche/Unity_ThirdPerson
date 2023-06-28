using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    // referencing something inside of a state
    // get reference (make it a property)
    // [field: SerializeField] - use for porperty so can be seen in Unity
    [field: SerializeField] public InputReader InputReader { get; private set; }

    private void Start()
    {
        // Go to PlayerTestState
        SwitchState(new PlayerTestState(this));
    }
}
