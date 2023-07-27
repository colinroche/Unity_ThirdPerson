using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // used to base through data (Target)
    public event Action<Target> OnDestroyed;

    private void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }
}
