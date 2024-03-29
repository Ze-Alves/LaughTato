using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovePlayer : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _airSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private LayerMask _whatIsGround;

    private Rigidbody _rb;
    private Vector3 moveDirection;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float xMov = Input.GetAxis("Horizontal");
        float zMov = Input.GetAxis("Vertical");

        moveDirection = new Vector3(xMov, 0.0f, zMov).normalized;

        if (moveDirection.magnitude > 0.1)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space) && CheckGrounded())
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    private bool CheckGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, .2f, _whatIsGround);
    }

    private void FixedUpdate()
    {
        if (moveDirection.magnitude > 0)
        {
            _rb.MovePosition(transform.position + moveDirection * _speed * Time.deltaTime);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * .2f);
    }
}
