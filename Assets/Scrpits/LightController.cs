using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {

    public Transform PlanetCenter;
    public float Radius = 7;
    public float Speed;

	// Use this for initialization
	void Start ()
    {
        gameObject.transform.position = new Vector3(0.0f,Radius,0.0f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.RotateAround(PlanetCenter.position, new Vector3(0, 0, 1),Speed);
    }
}
