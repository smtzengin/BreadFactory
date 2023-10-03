using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform lookAt;
    public Transform player;
    public float rotationSpeed = 2.0f;
    public float maxVerticalAngle = 45.0f;
    public float minVerticalAngle= 15.0f;

    private Transform cameraTransform;
    private float currentX = 0.0f;
    private float currentY = 0.0f;

    private void Start()
    {
        cameraTransform = transform;
    }

    private void LateUpdate()
    {              
        currentY = Mathf.Clamp(currentY, minVerticalAngle, maxVerticalAngle);

        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 negDistance = new Vector3(0, 0, -player.GetComponent<PlayerMovement>().distanceFromPlayer);
        Vector3 position = rotation * negDistance + lookAt.position;

        cameraTransform.position = position;
        cameraTransform.LookAt(lookAt.position);
    }

    public void RotateCamera(float rotateX, float rotateY)
    {
        currentX += rotateX * rotationSpeed;
        currentY += rotateY * rotationSpeed;
        currentY = Mathf.Clamp(currentY, minVerticalAngle, maxVerticalAngle);
    }
}
