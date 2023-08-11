using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float drag;

    private Vector3 dampingVelocity;
    private Vector3 impact;
    private float verticalVelocity;

    public Vector3 Movement => impact + Vector3.up * verticalVelocity;

    private void Update()
    {
        if (verticalVelocity < 0f && controller.isGrounded)
        {
            // Setting gravity
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            // Adding gravity
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        // Slowly reduce impact to 0
        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);

        if (agent != null)
        {
            if (impact == Vector3.zero)
            {
                agent.enabled = true;
            }
        }
       
    }

    public void AddForce(Vector3 force)
    {
        impact += force;
        if (agent != null) 
        {
            agent.enabled = false;
        }
    }
}
