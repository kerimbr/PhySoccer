using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _playerRigidbody;

    [SerializeField] private Transform orientationTransform;
    

    private float
        _verticalInput,
        _horizontalInput;


    private Vector3 _moveDirection;

    [Header("Movement Settings")] [SerializeField]
    private float moveSpeed = 10f;

    [Header("Jump Settings")] [SerializeField]
    private KeyCode jumpKey;

    [SerializeField] private float jumpForce;
    [SerializeField] private bool canJump;
    [SerializeField] private float jumpCooldown;

    [Header("Ground Check Settings")] [SerializeField]
    private LayerMask groundLayer;

    [SerializeField] private float playerHeight;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerRigidbody.freezeRotation = true;
        canJump = true;
    }

    private void Update()
    {
        SetInputs();
    }

    private void FixedUpdate()
    {
        SetPlayerMovement();
    }

    private void SetInputs()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
        Debug.Log("canJump is" + canJump);
        Debug.Log("Isgrounded is" + IsGrounded());
        if (Input.GetKey(jumpKey) && canJump && IsGrounded())
        {
            canJump = false;
            SetPlayerJump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void SetPlayerJump()
    {
        _playerRigidbody.linearVelocity =
            new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);
        _playerRigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void SetPlayerMovement()
    {
        _moveDirection = orientationTransform.forward * _verticalInput + orientationTransform.right * _horizontalInput;
        _playerRigidbody.AddForce(_moveDirection.normalized * moveSpeed, ForceMode.Force);
    }

    private void ResetJump()
    {
        canJump = true;
    }

    private bool IsGrounded()
    {
        Debug.DrawRay( new Vector3(transform.position.x, transform.position.y + playerHeight, transform.position.z),
            Vector3.down, Color.red);
        // detect is player grounded
        return Physics.Raycast(
             new Vector3(transform.position.x, transform.position.y + playerHeight, transform.position.z),
            Vector3.down,
            playerHeight,
            groundLayer
            );
    }
}