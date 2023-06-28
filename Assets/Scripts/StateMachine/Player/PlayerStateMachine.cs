using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{

    private void Start()
    {
        // Go to PlayerTestState
        SwicthState(new PlayerTestState(this));
    }
}
