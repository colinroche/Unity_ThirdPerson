using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

// Data for each attack
public class Attack
{
    [field: SerializeField] public string AnimationName { get; private set; }
}
