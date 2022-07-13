using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManipulator : MonoBehaviour
{
    [HideInInspector] public Transform cup;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball")
        {
            other.transform.SetParent(cup);
            var rb = other.GetComponent<Rigidbody>();
            rb.useGravity = false;
            other.GetComponent<BallSettings>().isBallOnAir = true;
        }
    }

    public void setCup(Transform cup)
    {
        this.cup = cup;

    }
}
