using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Player : MonoBehaviour
{
    public float SpeedPlanet;
    public Rigidbody2D TargetRotation;

    public bool IsGrounded;
    public bool IsJumping;
    public bool IsDescending;
    public bool IsRunning;
    public bool IsIdle;

    public Transform RayOrigin;
    public float rayCheckDistance;
    public float JumpForce;
    public ForceMode2D JumpForceMode;
    public LayerMask ground;

    public float SpeedDebug;
    public float MinSpeed;
    public float DescendingForce;
    public ForceMode2D descendingForceMode;

    private Animator _animator;

    private Rigidbody2D _rigidBody;

    private Vector3 _leftScale;
    private Vector3 _rightScale;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        _animator = GetComponent<Animator>();

        _leftScale = transform.localScale;
        _leftScale.x *= -1;

        _rightScale = transform.localScale;
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        CheckForGround();

        if (IsJumping && !IsDescending)
        {
            SpeedDebug = _rigidBody.velocity.y;

            if (SpeedDebug < MinSpeed)
            {
                IsDescending = true;

                //_rigidBody.velocity = Vector2.zero;

                _rigidBody.AddForce(Vector2.down*DescendingForce, descendingForceMode);
                _animator.SetTrigger("Descending");
                //_rigidBody.velocity = Vector2.down * DescendingForce;
            }
        }
        else if (!IsJumping && !IsGrounded && !IsDescending)
        {
            IsDescending = true;

            //_rigidBody.velocity = Vector2.zero;

            _rigidBody.AddForce(Vector2.down * DescendingForce, descendingForceMode);
            _animator.SetTrigger("Descending");
            //_rigidBody.velocity = Vector2.down * DescendingForce;
        }

        float horizontal = (int) Input.GetAxisRaw("Horizontal");

        if (horizontal != 0)
        {
            if (horizontal < 0)
            {
                TargetRotation.rotation += -SpeedPlanet * Time.deltaTime;
                transform.localScale = _leftScale;
            }
            else if (horizontal > 0)
            {
                TargetRotation.rotation += SpeedPlanet * Time.deltaTime;
                transform.localScale = _rightScale;
            }

            if (!IsRunning && IsGrounded)
            {
                _animator.SetTrigger("Run");
                IsRunning = true;
                IsIdle = false;
            }
        }
        else
        {
            IsRunning = false;

            if (IsGrounded && !IsJumping && !IsIdle)
            {
                _animator.SetTrigger("Idle");
                IsIdle = true;
            }
        }

        if (Input.GetAxis("Jump") > 0 && IsGrounded && !IsJumping && !IsDescending)
        {
            Jump();
        }
    }

    private void CheckForGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(RayOrigin.transform.position, TargetRotation.position, rayCheckDistance, ground);

        if (hit.collider != null)
        {
            IsGrounded = true;
            IsJumping = false;
            IsDescending = false;

            //_rigidBody.velocity = Vector2.zero;
        }
        else
        {
            IsGrounded = false;
            IsRunning = false;
            IsIdle = false;
            IsRunning = false;
        }
    }

    private void Jump()
    {
        Debug.Log("Jump");

        _rigidBody.velocity = Vector2.zero;
        _rigidBody.AddForce(Vector2.up*JumpForce, JumpForceMode);
        //_rigidBody.velocity = Vector2.up * JumpForce;
        IsJumping = true;
        IsDescending = false;

        _animator.SetTrigger("Jump");
    }
}