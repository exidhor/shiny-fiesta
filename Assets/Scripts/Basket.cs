using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Basket : MonoBehaviour
{
    public int Amount;
    public Player Player;

    public WeedType WeedType;

    public Sprite VoidSprite;
    public Sprite RedSprite;
    public Sprite BlueSprite;
    public Sprite PurpleSprite;

    private SpriteRenderer _renderer;

    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        Weed weed = collider.GetComponent<Weed>();

        if (weed != null && weed.IsPickable)
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

                switch (WeedType)
                {
                    case WeedType.Blue:
                        _renderer.sprite = BlueSprite;
                        break;

                    case WeedType.Purple:
                        _renderer.sprite = PurpleSprite;
                        break;

                    case WeedType.Red:
                        _renderer.sprite = RedSprite;
                        break;
                }
            }

            return;
        }

        Dealer dealer = collider.GetComponent<Dealer>();

        if (dealer != null)
        {
            if (Amount > 0)
            {
                Player.score += dealer.Sell(Amount, WeedType);
                Amount = 0;

                _renderer.sprite = VoidSprite;
            }   
        }
    }
}
