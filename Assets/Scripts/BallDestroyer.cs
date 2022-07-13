using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class BallDestroyer : MonoBehaviour
{
    [SerializeField] private Vector3 size;
    [SerializeField] private LayerMask ballLayerMask;
    [SerializeField] private GameMode gameMode;
    private static int destroyedBallCount;

    private void Start()
    {
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        destroyedBallCount = 0;
    }

    private void FixedUpdate()
    {
        Collider[] ballColliders = Physics.OverlapBox(transform.position, size, Quaternion.identity, ballLayerMask);
        int ctr = 0;
        for (int i = ballColliders.Length-1; i >= 0; i--)
        {
            DestroyImmediate(ballColliders[i].gameObject);
            ctr++;
        }
        if (ctr > 0)
        {
            destroyedBallCount += ctr;
            gameMode.OnBallDestroy(destroyedBallCount);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
