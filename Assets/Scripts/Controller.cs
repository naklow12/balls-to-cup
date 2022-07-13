using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Controller : MonoBehaviour
{
    Vector3 previousPos;
    Vector3 direction;
    float magnitude;
    [Header("Sensitivity Settings")]
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float acceleration = 1f;
    [SerializeField] private float maxSensitivity = 4f;
    [SerializeField] private float breakSpeed = 10f;
    [SerializeField] private float threshold = 5f;
    [SerializeField] private float gravityForce = -9.81f;


    private void Update()
    {
        inputHandler();
    }

    private void inputHandler()
    {
        if (Input.GetMouseButton(0))
        {
            if (Mathf.Abs(Input.mousePosition.x - previousPos.x) > threshold)
            {
                direction = (Input.mousePosition - previousPos) / 2;
                direction = Vector3.ClampMagnitude(direction, maxSensitivity);
                magnitude = Mathf.Lerp(magnitude, direction.x, Time.deltaTime * acceleration);
            }
            else
            {
                direction = Vector3.zero;
                magnitude = Mathf.Lerp(magnitude, 0, Time.deltaTime * breakSpeed);
            }
        }
        else
        {
            direction = Vector3.zero;
            magnitude = Mathf.Lerp(magnitude, 0, Time.deltaTime * breakSpeed);
        }
        previousPos = Input.mousePosition;
    }


    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0f, 0f, -magnitude * rotationSpeed) * Time.deltaTime);
        Physics.gravity = transform.up * gravityForce;
    }
}
