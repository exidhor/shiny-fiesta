using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.GetComponent<Player>() != null)
        {
            Player player = coll.GetComponent<Player>();
            player.IsNearWell = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.GetComponent<Player>() != null)
        {
            Player player = coll.GetComponent<Player>();
            player.IsNearWell = false;
        }
    }
}
