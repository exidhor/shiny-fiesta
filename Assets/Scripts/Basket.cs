using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Basket : MonoBehaviour
{
    public int Amount;
    public Player Player;

    public WeedType WeedType;

    void OnTriggerStay2D(Collider2D collider)
    {
        Weed weed = collider.GetComponent<Weed>();

        if (weed != null)
        {
            if (Amount > 0)
            {
                if (weed.WeedType == WeedType)
                {
                    Amount++;
                    Destroy(collider.gameObject);
                }
            }
            else
            {
                WeedType = weed.WeedType;
                Amount++;
                Destroy(collider.gameObject);
            }

            return;
        }

        Dealer dealer = collider.GetComponent<Dealer>();

        if (dealer != null)
        {
            if (Amount > 0)
            {
                Player.score += dealer.Sell(Amount, WeedType);
            }   
        }
    }
}
