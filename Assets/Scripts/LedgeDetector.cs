using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeDetector : MonoBehaviour
{
    public event Action<Vector3, Vector3> OnLedgeDetect;

    public void OnTriggerEnter(Collider other)
    {
        // Invoke(where did our hands touch the ledge, direction of ledege to hands)
        OnLedgeDetect?.Invoke(other.ClosestPoint(transform.position), other.transform.forward);
    }
}
