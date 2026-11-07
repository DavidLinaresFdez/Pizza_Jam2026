using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float dashSpeed;
    private bool isDashing;
    [SerializeField]private float dashDuration;

    [SerializeField] private Rigidbody2D rigidbody;
    private InputSystem_Actions inputActions;

    private Vector2 moveInput;
    

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Enable();

        //Move
        inputActions.Player.Move.performed += OnPlayerMoveKeysPressed;
        inputActions.Player.Move.canceled += OnPlayerMoveKeysReleased;

        //Dash
        inputActions.Player.Dash.performed += OnPlayerDasPerformed;
        inputActions.Player.Dash.canceled += OnPlayerDashCanceled;
    }

    private void OnPlayerDashCanceled(InputAction.CallbackContext context)
    {
       

        StopCoroutine(DashRoutine(moveInput, speed));

    }

    private void OnPlayerDasPerformed(InputAction.CallbackContext context)
    {
        StartCoroutine(DashRoutine(moveInput, speed));
    }

    private void OnPlayerMoveKeysPressed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        UpdatePosition(moveInput, speed);
    }

    private void OnPlayerMoveKeysReleased(InputAction.CallbackContext context)
    {
        moveInput = new Vector2(0, 0);
        UpdatePosition(moveInput, speed);
       
    }

    private void UpdatePosition(Vector2 movementVector, float playerSpeed)
    {
        rigidbody.linearVelocity = movementVector*playerSpeed;
    }

    private IEnumerator DashRoutine(Vector2 direction, float playerSpeed)
    {
        isDashing = true;
        float elapsed = 0f;
        while (elapsed < dashDuration/60)
        {
            rigidbody.linearVelocity = moveInput+direction * dashSpeed;
            elapsed += Time.deltaTime;
            yield return null;
        }
        rigidbody.linearVelocity = moveInput * playerSpeed;
        isDashing = false;
    }


    private void OnDestroy()
    {
        inputActions.Disable();
        inputActions.Player.Move.performed -= OnPlayerMoveKeysPressed;
        inputActions.Player.Move.canceled -= OnPlayerMoveKeysReleased;
    }

}
