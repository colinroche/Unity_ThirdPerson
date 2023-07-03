using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    // referencing something inside of a state
    // get reference (make it a property)
    // [field: SerializeField] - use for porperty so can be seen in Unity
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
    public Transform MainCameraTransform { get; private set; }

    private void Start()
    {
        MainCameraTransform = Camera.main.transform;

        // Go to PlayerTestState
        SwitchState(new PlayerTestState(this));
    }
}
