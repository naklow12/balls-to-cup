using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tube : MonoBehaviour
{
    [SerializeField] private GravityManipulator gravityManipulator;
    [SerializeField] private Collider spawnArea;
    [SerializeField] private Color[] ballColors;

    public void generateBalls(int count,GameObject ball)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject newBall = Instantiate(ball,getSpawnArea(),Quaternion.identity);   
            newBall.GetComponent<MeshRenderer>().material.color = ballColors[Random.Range(0, ballColors.Length)];
        }
    }

    private Vector3 getSpawnArea()
    {
        var bounds = spawnArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        float z = Random.Range(bounds.min.z, bounds.max.z);
        return new Vector3 (x, y, z);
    }

    public void setTargetCup(Transform cup)
    {
        gravityManipulator.setCup(cup);
    }
}
