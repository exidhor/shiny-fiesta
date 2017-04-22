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
            SpriteRenderer[] renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
            for(int i = 0; i < renderers.GetLength(0); ++i)
            {
                Color tmp = renderers[i].color;
                tmp.a = 0;
                renderers[i].color = tmp;
            }
        }
    }

    // Use this for initialization
    void Start () {
        timeElapsed = 0;
        active = true;
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
                SpriteRenderer[] renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
                for (int i = 0; i < renderers.GetLength(0); ++i)
                {
                    Color tmp = renderers[i].color;
                    tmp.a = 255;
                    renderers[i].color = tmp;
                }
                active = true;
            }
        }

    }
}
