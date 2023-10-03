using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Camera mainCamera;
    public float speed = 5.0f;
    public float distanceFromPlayer = 2.0f; // Kamera ile karakter arasındaki mesafe
    public float rotationSpeed = 5f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        mainCamera = Camera.main; // Ana kamera referansını al        
    }

    public void Move(float horizontal, float vertical)
    {
        // Kameranın bakış yönünü al
        Vector3 cameraForward = mainCamera.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize(); 

        // Hareketi kameranın bakış açısına göre ayarla
        Vector3 moveDirection = cameraForward * vertical + mainCamera.transform.right * horizontal;

        // Karakterin rotasyonunu kameraya göre ayarla.
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }        
        controller.SimpleMove(moveDirection * speed);
    }
}
