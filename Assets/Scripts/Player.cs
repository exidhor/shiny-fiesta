using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public bool IsGrounded;

    public Transform RayOrigin;
    public float rayCheckDistance;
    public float JumpForce;
    public LayerMask ground;

    private Rigidbody2D _rigidBody;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetAxis("Jump") > 0 && CheckForGround())
        {
            Jump();
        }
    }

    private bool CheckForGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(RayOrigin.transform.position, Vector2.down, rayCheckDistance, ground);

        IsGrounded = (hit.collider != null);

        return IsGrounded;
    }

    private void Jump()
    {
        _rigidBody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }
}