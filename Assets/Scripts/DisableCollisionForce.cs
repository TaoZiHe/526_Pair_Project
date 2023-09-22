using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCollisionForce : MonoBehaviour
{

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // private void OnCollisionEnter(Collision other)
    // {
    //     rb.velocity = Vector3.zero;
    //     rb.angularVelocity = Vector3.zero;
    // }
    
    void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(StopMotionAfterCollision());
    }

    IEnumerator StopMotionAfterCollision()
    {
        rb.isKinematic = true;
        yield return new WaitForFixedUpdate();  // Wait for physics calculations
        rb.isKinematic = false;
    }
}
