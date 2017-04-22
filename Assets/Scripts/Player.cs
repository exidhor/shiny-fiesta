using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public bool IsGrounded;
    public bool IsJumping;
    public bool IsDescending;

    public Transform RayOrigin;
    public float rayCheckDistance;
    public float JumpForce;
    public ForceMode2D JumpForceMode;
    public LayerMask ground;

    public float Speed;
    public float MinSpeed;
    public float DescendingForce;
    public ForceMode2D descendingForceMode;

    private Rigidbody2D _rigidBody;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetAxis("Jump") > 0 && IsGrounded && !IsJumping)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        CheckForGround();

        if (IsJumping && !IsDescending)
        {
            Speed = _rigidBody.velocity.y;

            if (Speed < MinSpeed)
            {
                IsDescending = true;

                //_rigidBody.velocity = Vector2.zero;

                _rigidBody.AddForce(Vector2.down*DescendingForce, descendingForceMode);
                //_rigidBody.velocity = Vector2.down * DescendingForce;
            }
        }
    }

    private void CheckForGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(RayOrigin.transform.position, Vector2.down, rayCheckDistance, ground);

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
    }
}