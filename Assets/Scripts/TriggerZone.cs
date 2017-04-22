using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TriggerZone : MonoBehaviour
{
    private Collider2D[] _collider;

    public bool Collision;

    void Awake()
    {
        _collider = GetComponents<Collider2D>();
        Collision = false;
    }

    void FixedUpdate()
    {
        Collision = false;
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        Collision = true;
    }
}