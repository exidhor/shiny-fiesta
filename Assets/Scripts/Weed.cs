using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Weed : MonoBehaviour
{
    public int CurrentWater;
    public int MaxWater = 300;
    public Sprite[] sprites = new Sprite[3];
    public bool IsOnTheGround = false;

    public WeedType WeedType;

    public bool IsPickable
    {
        get { return CurrentWater == MaxWater; }
    }

    public void ReceiveWater(int waterAmount)
    {
        if (IsOnTheGround)
        {
            CurrentWater += waterAmount;
            if (CurrentWater >= MaxWater)
                CurrentWater = MaxWater;
        }
    }

    void Update()
    {
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        if (CurrentWater < MaxWater/2)
            renderer.sprite = sprites[0];
        else if (CurrentWater < MaxWater)
            renderer.sprite = sprites[1];
        else
        {
            renderer.sprite = sprites[2];

        }
    }

    void Start()
    {
        IsOnTheGround = false;
        CurrentWater = 0;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.GetComponent<Player>() != null)
        {
            Player player = coll.GetComponent<Player>();
            if(player.WeedInContact == null)
                player.WeedInContact = this;
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.GetComponent<Player>() != null)
        {
            Player player = coll.GetComponent<Player>();
            if (player.WeedInContact == null)
                player.WeedInContact = this;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.GetComponent<Player>() != null)
        {
            Player player = coll.GetComponent<Player>();
            if (player.WeedInContact == this)
                player.WeedInContact = null;
        }
    }
}