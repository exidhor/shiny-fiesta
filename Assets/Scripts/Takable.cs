using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Takable : MonoBehaviour
{
    private Player _player;

    void FixedUpdate()
    {
        if (_player != null)
        {
            _player.Take(this);
        }

        _player = null;
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        Player player = collider.GetComponent<Player>();

        if (player != null)
        {
            _player = player;
        }
    }
}