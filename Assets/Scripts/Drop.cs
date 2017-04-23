using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public GameObject Target;
    public float Speed;
    public int WaterAmount;

    private Rigidbody2D _rigidbody;

    private float _offset;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetOffset(float offset)
    {
        _offset = offset;
    }

    void FixedUpdate()
    {
        transform.localRotation = Quaternion.identity;

        Vector3 position = transform.localPosition;

        position.y -= Speed*Time.fixedDeltaTime;
        position.x = _offset;

        transform.localPosition = position;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        Weed weed = coll.GetComponent<Weed>();

        if (weed != null)
        {
            weed.ReceiveWater(WaterAmount);
        }
        else
        {
            Bucket bucket = coll.GetComponent<Bucket>();

            if (bucket != null)
                bucket.Fill(WaterAmount);
        }

        Destroy(gameObject);
    }
}
