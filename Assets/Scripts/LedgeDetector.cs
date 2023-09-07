using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeDetector : MonoBehaviour
{
    public event Action<Vector3, Vector3> OnLedgeDetect;

    public void OnTriggerEnter(Collider other)
    {
        // Closest point to the edge of the ledge, closest point to hands
        OnLedgeDetect?.Invoke(other.transform.forward, other.ClosestPointOnBounds(transform.position));
    }
}
