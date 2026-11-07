using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed = 3f;

    [SerializeField] private Rigidbody2D rigidbody;
    private InputSystem_Actions inputActions;

    private Vector2 moveInput;

    

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Enable();
        inputActions.Player.Move.performed += OnPlayerMoveKeysPressed;
        inputActions.Player.Move.canceled += OnPlayerMoveKeysReleased;
    }



    private void OnPlayerMoveKeysPressed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        UpdatePosition(moveInput);
    }

    private void OnPlayerMoveKeysReleased(InputAction.CallbackContext context)
    {
        moveInput = new Vector2(0, 0);
        UpdatePosition(moveInput);
       
    }

    private void UpdatePosition(Vector2 movementVector)
    {
        rigidbody.linearVelocity = movementVector*speed;
    }

    private void OnDestroy()
    {
        inputActions.Disable();
        inputActions.Player.Move.performed -= OnPlayerMoveKeysPressed;
        inputActions.Player.Move.canceled -= OnPlayerMoveKeysReleased;
    }

}
