using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollecter : MonoBehaviour
{
    [SerializeField] private LayerMask ballLayerMask;
    [SerializeField] private float radius;
    [SerializeField] private GameMode gameMode;
    private int collectedBallCount;
    
    private void FixedUpdate()
    {
        Collider[] ballColliders = Physics.OverlapSphere(transform.position, radius, ballLayerMask);
        
        for (int i = ballColliders.Length - 1; i >= 0; i--)
        {
            ballColliders[i].gameObject.layer = 0>>1;
            collectedBallCount++;
        }
        if (ballColliders.Length > 0)
            gameMode.OnBallCollected(collectedBallCount);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,radius);
    }

}
