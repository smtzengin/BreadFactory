using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private DynamicJoystick joystick;
    [SerializeField, Range(1, 10)] private float _characterSpeed;
    [SerializeField, Range(1, 10)] private float _rotationSpeed;

    private Animator playerAnim;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerAnim = GetComponent<Animator>();
    }

    private void Update()
    {        
        Move(_characterSpeed);
    }


    private void Move(float speed)
    {
        var movementDirection = new Vector3(joystick.Direction.x,0f,joystick.Direction.y);
        characterController.SimpleMove(movementDirection * speed);

        if (movementDirection.sqrMagnitude <= 0)
        {
            playerAnim.SetBool("isRun", false);
            return;
        }
        playerAnim.SetBool("isRun", true);    

        var targetDirection = Vector3.RotateTowards(transform.forward, movementDirection, _rotationSpeed * Time.deltaTime, 0.0f);

        transform.rotation = Quaternion.LookRotation(targetDirection);
    }
}
