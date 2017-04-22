using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class InputMovement : MonoBehaviour
{
    public float Speed;

    private Rigidbody2D _rigidBody;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = 0;

        horizontal = (int)Input.GetAxisRaw("Horizontal");

        if(horizontal < 0)
            _rigidBody.rotation += -Speed * Time.deltaTime;

        else if(horizontal > 0)
            _rigidBody.rotation += Speed * Time.deltaTime;

    }
}