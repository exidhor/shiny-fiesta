using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Barn : MonoBehaviour
{
    public Weed WeedPrefab;

    public int Price;

    private Weed nextWeed;

    private Player _player;

    void FixedUpdate()
    {
        if (_player)
        {
            if (_player.Buy(nextWeed.GetComponent<Takable>(), Price))
            {
                nextWeed.gameObject.SetActive(true);
                nextWeed = null;
            }
        }

        if (nextWeed == null)
        {
            nextWeed = Instantiate(WeedPrefab);
            nextWeed.gameObject.SetActive(false);
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