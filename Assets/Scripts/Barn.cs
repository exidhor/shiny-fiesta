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

    void FixedUpdate()
    {
        if (nextWeed == null)
        {
            nextWeed = Instantiate(WeedPrefab);
            nextWeed.gameObject.SetActive(false);
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        Player player = collider.GetComponent<Player>();

        if (player != null)
        {
            if (player.Buy(nextWeed.GetComponent<Takable>(), Price))
            {
                nextWeed.gameObject.SetActive(true);
                nextWeed = null;
            }
        }
    }
}