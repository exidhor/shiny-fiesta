using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {

    public Transform PlanetCenter;
    public float Radius = 7;
    public float Speed;
    public bool Inverted = false;

    public float ChangeDirectionTime;
    public float ChangeDirectionRate;
    private float _currentTime = 0;

	// Use this for initialization
	void Start ()
    {
        if(Inverted)
            gameObject.transform.position = new Vector3(0.0f, -Radius, 0.0f);
        else
            gameObject.transform.position = new Vector3(0.0f,Radius,0.0f);
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    _currentTime += Time.fixedDeltaTime;

	    if (_currentTime > ChangeDirectionTime)
	    {
	        float random = Random.value;

	        if (random < ChangeDirectionRate)
	        {
	            Speed *= -1;
	        }

	        _currentTime = 0;
	    }

        gameObject.transform.RotateAround(PlanetCenter.position, new Vector3(0, 0, 1),Speed);
    }
}
