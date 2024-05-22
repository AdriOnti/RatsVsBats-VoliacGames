using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRotate : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float maxRotationAngle = 45f;

    public bool rotateRight = true;
    private float currentRotation = 0f;

    void Update()
    {
        // Rotate to the right if rotateRight is true
        if (rotateRight)
        {
            // Calculate the target rotation angle
            float targetRotation = Mathf.Clamp(currentRotation + rotationSpeed * Time.deltaTime, -maxRotationAngle, maxRotationAngle);

            // Rotate the object
            transform.Rotate(0f, targetRotation - currentRotation, 0f);

            // Update the current rotation
            currentRotation = targetRotation;

            // If we reach the max rotation angle, switch direction
            if (currentRotation >= maxRotationAngle)
            {
                rotateRight = false;
                SoundManager.Instance.PlayEffect(Audios.effectMonsterIdle);
            }
        }
        // Rotate to the left if rotateRight is false
        else
        {
            // Calculate the target rotation angle
            float targetRotation = Mathf.Clamp(currentRotation - rotationSpeed * Time.deltaTime, -maxRotationAngle, maxRotationAngle);

            // Rotate the object
            transform.Rotate(0f, targetRotation - currentRotation, 0f);

            // Update the current rotation
            currentRotation = targetRotation;

            // If we reach the min rotation angle, switch direction
            if (currentRotation <= -maxRotationAngle)
            {
                rotateRight = true;
            }
        }
    }
}