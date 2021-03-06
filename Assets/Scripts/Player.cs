﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Player : MonoBehaviour
{
    public Transform TakableContainer;
    public Transform Ground;

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

    public Transform BucketDisplay;

    private Animator _animator;

    private Rigidbody2D _rigidBody;

    private Vector3 _leftScale;
    private Vector3 _rightScale;

    private GameObject _takable;

    public int score;
    public Text scoreText;

    public Weed WeedInContact;
    public bool IsNearWell;

    private bool _takeSomethingThisFrame;

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

    void Start()
    {
        //score = 0;
        WeedInContact = null;
        IsNearWell = false;
    }

    void Update()
    {
        if (scoreText != null)
        {
            string str = score.ToString();
            while (str.Length < 5)
                str = '0' + str;
            scoreText.text = str;
        }
    }

    void FixedUpdate()
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
            if (horizontal < 0/* && !TriggerLeft.Collision*/)
            {
                TargetRotation.rotation += -SpeedPlanet*Time.deltaTime;
                transform.localScale = _leftScale;

                TriggerRight.transform.localScale = _leftScale;
                TriggerLeft.transform.localScale = _leftScale;

                BucketDisplay.transform.localScale = _leftScale;

                if (_takable != null)
                {
                    Takable takable = _takable.GetComponent<Takable>();

                    if (takable != null)
                    {
                        GameObject textInfo = takable.TextInfo;

                        if (textInfo != null)
                        {
                            textInfo.transform.localScale = _leftScale;
                        }
                    }
                }

            }
            else if (horizontal > 0/* && !TriggerRight.Collision*/)
            {
                TargetRotation.rotation += SpeedPlanet*Time.deltaTime;
                transform.localScale = _rightScale;

                TriggerRight.transform.localScale = _rightScale;
                TriggerLeft.transform.localScale = _rightScale;

                BucketDisplay.transform.localScale = _rightScale;

                if (_takable != null)
                {
                    Takable takable = _takable.GetComponent<Takable>();

                    if (takable != null)
                    {
                        GameObject textInfo = takable.TextInfo;

                        if (textInfo != null)
                        {
                            textInfo.transform.localScale = _rightScale;
                        }
                    }
                }
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
            _animator.SetTrigger("Fall");
            _lastDescending = true;
            IsDescending = true;
        }

        if (_takable && !_takeSomethingThisFrame && InputManager.Instance.Action)
        {
            Release();
        }

        _takeSomethingThisFrame = false;
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

        _rigidBody.AddForce(Vector2.down*DescendingForce, descendingForceMode);

        //Debug.Log("IsDescending");
    }

    private void Jump()
    {
        //Debug.Log("Jump");

        _rigidBody.velocity = Vector2.zero;
        _rigidBody.AddForce(Vector2.up*JumpForce, JumpForceMode);

        ResetBuffers();
        IsJumping = true;
    }

    private void Run()
    {
        ResetBuffers();
        IsRunning = true;

        //Debug.Log("Run");
    }

    private void Idle()
    {
        ResetBuffers();
        IsIdle = true;

        //Debug.Log("idle");
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

    public void PickPickUp(PickUp pickUp)
    {
        switch (pickUp.Type)
        {
            case PickUp.TypePickUp.score:
                score += 10;
                break;
            case PickUp.TypePickUp.speedBoost:
                // TO DO
                break;

            default:
                break;
        }
    }

    public bool Buy(Takable takable, int price)
    {
        if (price <= score)
        {
            if (Take(takable))
            {
                score -= price;

                Destroy(takable);
                return true;
            }
        }

        return false;
    }

    public bool Take(Takable takable)
    {
        if (_takable == null && IsGrounded && InputManager.Instance.Action)
        {
            GameObject textInfo = takable.TextInfo;

            if (textInfo != null)
            {
                takable.TextInfo.SetActive(false);
            }

            _takeSomethingThisFrame = true;

            _takable = takable.gameObject;

            _takable.transform.parent = TakableContainer;
            _takable.transform.position = TakableContainer.position;
            _takable.transform.rotation = TakableContainer.rotation;

            return true;
        }

        return false;
    }

    public void Release()
    {
        if (!IsGrounded)
            return;

        if (WeedInContact != null && _takable != null && _takable.gameObject.GetComponent<Bucket>() != null && !_takable.gameObject.GetComponent<Bucket>().IsCorupted)
        {
            WeedInContact.ReceiveWater(_takable.gameObject.GetComponent<Bucket>().FillLevel);
            _takable.gameObject.GetComponent<Bucket>().FillLevel = 0;

            return;
        }

        if(IsNearWell && _takable != null && _takable.gameObject.GetComponent<Bucket>() != null)
        {
            _takable.gameObject.GetComponent<Bucket>().FillLevel = 0;
            _takable.gameObject.GetComponent<Bucket>().IsCorupted = false;
            _takable.gameObject.GetComponent<Bucket>().progress.IsCorrupted = false;
            return;
        }

        _takable.transform.parent = TargetRotation.transform;
        _takable.transform.position = Ground.position;

        if (_takable.gameObject.GetComponent<Weed>() != null)
        {
            _takable.gameObject.GetComponent<Weed>().IsOnTheGround = true;
        }
        
        Takable takable = _takable.GetComponent<Takable>();

        if (takable != null)
        {
            GameObject textInfo = takable.TextInfo;

            if (textInfo != null)
            {
                textInfo.SetActive(true);
            }
        }

        _takable = null;
    }
}