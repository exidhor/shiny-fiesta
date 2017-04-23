using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour {

    public int FillBucketQuantity = 1;
    public float TimeUntilNextFill = 0.5f;
    float timeElapsed = 0.0f;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.CompareTag("Weed"))
        {

        } 
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Seau"))
        {
            timeElapsed += Time.fixedDeltaTime;
            if (timeElapsed >= TimeUntilNextFill)
            {
                Bucket bucket = coll.gameObject.GetComponent<Bucket>();
                bucket.Fill(FillBucketQuantity);
                timeElapsed = 0.0f;
            }
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Seau"))
        {
            timeElapsed = 0.0f;
        }
    }

    // Use this for initialization
    void Start () {
        timeElapsed = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
