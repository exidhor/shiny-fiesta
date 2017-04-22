using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class InputMovement : MonoBehaviour
{
    public float Speed;

    public Rigidbody2D Target;

    void Awake()
    {
        Target = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = 0;

        horizontal = (int)Input.GetAxisRaw("Horizontal");

        if(horizontal < 0)
            Target.rotation += -Speed * Time.deltaTime;

        else if(horizontal > 0)
            Target.rotation += Speed * Time.deltaTime;

    }
}