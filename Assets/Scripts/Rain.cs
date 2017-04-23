using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour {

    public int FillBucketQuantity = 1;
    public float TimeUntilNextFill = 0.5f;
    float timeElapsed = 0.0f;

    public float Range;

    public GameObject rainPrefab;

    public float RainTime;
    public float rainTimer = 0f;

    public GameObject Planet;

    public Transform DropContainer;

    void Update()
    {
        rainTimer += Time.deltaTime;

        if (RainTime < rainTimer)
        {
            rainTimer = 0;
            InstantiateRain();
        }
    }

    private GameObject InstantiateRain()
    {
        Drop rain = Instantiate(rainPrefab).GetComponent<Drop>();
        rain.transform.parent = transform.transform;
        rain.Target = Planet;

        rain.transform.rotation = transform.rotation;
        rain.transform.position = transform.position;

        float offsetX = Random.Range(-Range, Range);

        rain.transform.localPosition = new Vector3(offsetX, 0, 0);

       rain.SetOffset(offsetX);

        return rain.gameObject;
    }

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
}
