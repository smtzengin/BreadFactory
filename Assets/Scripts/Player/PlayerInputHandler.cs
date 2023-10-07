using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private FixedJoystick leftJoystick;
    [SerializeField] private FixedJoystick rightJoystick;

    private PlayerMovement playerMovement;
    private CameraMove cameraMove;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        cameraMove = Camera.main.GetComponent<CameraMove>();
    }

    private void Update()
    {
        // leftJoystick ile karakter hareketleri.
        float horizontal = leftJoystick.Horizontal;
        float vertical = leftJoystick.Vertical;
        playerMovement.Move(horizontal, vertical);
        playerMovement.anim.SetFloat("floatZ", vertical);
        playerMovement.anim.SetFloat("floatX", horizontal);

        // rightJoystick ile kamera rotasyonu.
        float rotateX = rightJoystick.Horizontal;
        float rotateY = rightJoystick.Vertical;
        cameraMove.RotateCamera(rotateX, rotateY);
    }
}
