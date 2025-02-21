using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody _playerRigidbody;

    [SerializeField] private Transform orientationTransform;
    
    private float _verticalInput, _horizontalInput;
    
    private Vector3 _moveDirection;
    
    
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 10f;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerRigidbody.freezeRotation = true;
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
    }
    
    private void SetPlayerMovement()
    {
        _moveDirection = orientationTransform.forward * _verticalInput + orientationTransform.right * _horizontalInput;
        _playerRigidbody.AddForce(_moveDirection.normalized * 10f, ForceMode.Force);
    }
    
    
}
