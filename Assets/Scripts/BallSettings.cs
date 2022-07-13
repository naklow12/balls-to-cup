using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSettings : MonoBehaviour
{
    Rigidbody rb;
    public bool isBallOnAir;
    [Header("Ball Settings")]
    [SerializeField] private float airGravityForce = -1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(isBallOnAir)
            rb.velocity += transform.parent.parent.up * airGravityForce;
    }
}
