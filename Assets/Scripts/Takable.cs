using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Takable : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D collider)
    {
        Player player = collider.GetComponent<Player>();

        if (player != null && Input.GetButton("Action"))
        {
            player.Take(this);
        }
    }
}