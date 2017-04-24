using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class TextInfo : MonoBehaviour
{
    void FixedUpdate()
    {
        if (transform.lossyScale.x < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }
}