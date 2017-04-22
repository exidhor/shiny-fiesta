using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

    public float TimeBeforeReactivation;
    public float timeElapsed = 0;
    bool active;
    public TypePickUp Type;

    public enum TypePickUp
    {
        score,
        speedBoost
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (active && other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.PickPickUp(this);
            active = false;
            Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
            tmp.a = 0;
            gameObject.GetComponent<SpriteRenderer>().color = tmp;
        }
    }

    // Use this for initialization
    void Start () {
        timeElapsed = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!active)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= TimeBeforeReactivation)
            {
                timeElapsed = 0;
                Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
                tmp.a = 255;
                gameObject.GetComponent<SpriteRenderer>().color = tmp;
                active = true;
            }
        }

    }
}
