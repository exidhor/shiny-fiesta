using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Player : MonoBehaviour
{
    public TriggerZone TriggerLeft;
    public TriggerZone TriggerRight;

    public float SpeedPlanet;
    public Rigidbody2D TargetRotation;

    public bool IsGrounded;
    public bool IsJumping;
    public bool IsDescending;
    public bool IsRunning;
    public bool IsIdle;

    private bool _lastGrounded = false;
    private bool _lastJumping = false;
    private bool _lastDescending = false;
    private bool _lastRunning = false;
    private bool _lastIdle = false;

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


    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;

        Vector3 offset = RayOrigin.position - TargetRotation.transform.position;
        offset.Normalize();
        offset *= rayCheckDistance;

        Gizmos.DrawLine(RayOrigin.position, RayOrigin.position - offset);
    }

    void Update()
    {
        CheckForGround();

        if (IsJumping && !IsDescending)
        {
            SpeedDebug = _rigidBody.velocity.y;

            if (SpeedDebug < MinSpeed)
            {
                Descending();
            }
        }
        else if (!IsJumping && !IsGrounded && !IsDescending)
        {
            Descending();
        }

        float horizontal = (int) Input.GetAxisRaw("Horizontal");

        if (horizontal != 0)
        {
            if (horizontal < 0 && !TriggerLeft.Collision)
            {
                TargetRotation.rotation += -SpeedPlanet*Time.deltaTime;
                transform.localScale = _leftScale;

                TriggerRight.transform.localScale = _leftScale;
                TriggerLeft.transform.localScale = _leftScale;
            }
            else if (horizontal > 0 && !TriggerRight.Collision)
            {
                TargetRotation.rotation += SpeedPlanet*Time.deltaTime;
                transform.localScale = _rightScale;

                TriggerRight.transform.localScale = _rightScale;
                TriggerLeft.transform.localScale = _rightScale;
            }

            if (!IsRunning && IsGrounded)
            {
                Run();
            }
        }
        else
        {
            IsRunning = false;

            if (IsGrounded && !IsJumping && !IsIdle)
            {
                Idle();
            }
        }

        if (Input.GetAxis("Jump") > 0 && IsGrounded && !IsJumping && !IsDescending)
        {
            Jump();
        }

        if (IsJumping && !_lastJumping)
        {
            ResetBuffers();
            _animator.SetTrigger("Jump");
            _lastJumping = true;
            IsJumping = true;
        }

        else if (IsRunning && !_lastRunning)
        {
            ResetBuffers();
            _animator.SetTrigger("Run");
            _lastRunning = true;
            IsRunning = true;
        }

        else if (IsIdle && !_lastIdle)
        {
            ResetBuffers();
            _animator.SetTrigger("Idle");
            _lastIdle = true;
            IsIdle = true;
        }

        else if (IsDescending && !_lastDescending)
        {
            ResetBuffers();
            _animator.SetTrigger("Descending");
            _lastDescending = true;
            IsDescending = true;
        }
    }

    private void CheckForGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(RayOrigin.transform.position, TargetRotation.position, rayCheckDistance,
            ground);

        if (hit.collider != null)
        {
            IsGrounded = true;
            IsJumping = false;
            IsDescending = false;
        }
        else
        {
            IsGrounded = false;
            IsRunning = false;
            IsIdle = false;
        }
    }

    private void Descending()
    {
        ResetBuffers();
        IsDescending = true;

        _rigidBody.AddForce(Vector2.down * DescendingForce, descendingForceMode);

        Debug.Log("IsDescending");
    }

    private void Jump()
    {
        Debug.Log("Jump");

        _rigidBody.velocity = Vector2.zero;
        _rigidBody.AddForce(Vector2.up*JumpForce, JumpForceMode);

        ResetBuffers();
        IsJumping = true;
    }

    private void Run()
    {
        ResetBuffers();
        IsRunning = true;

        Debug.Log("Run");
    }

    private void Idle()
    {
        ResetBuffers();
        IsIdle = true;

        Debug.Log("idle");
    }

    private void ResetBuffers()
    {
        _lastGrounded = false;
        _lastJumping = false;
        _lastDescending = false;
        _lastRunning = false;
        _lastIdle = false;

        IsJumping = false;
        IsDescending = false;
        IsIdle = false;
        IsRunning = false;
    }
}