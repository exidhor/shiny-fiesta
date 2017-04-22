using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    public bool Pop;
    public int CounterUntilTrigger;
    public Sprite SpriteToRender;

    public void Lap()
    {
        --CounterUntilTrigger;
        if(CounterUntilTrigger == 0)
        {
            if(Pop)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = SpriteToRender;
                gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = null;
                gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            }
        }
    }

	// Use this for initialization
	void Start () {
        if (Pop)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = SpriteToRender;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
