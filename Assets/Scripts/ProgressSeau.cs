﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressSeau : MonoBehaviour {

    public Sprite[] sprites = new Sprite[50];
    public Sprite CorruptedSprite;
    public int Max = 100;
    public int CurrentValue = 0;
    public bool IsCorrupted = false;
    
    // Use this for initialization
	void Start ()
    {
        CurrentValue = 0;
        IsCorrupted = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();

        if (IsCorrupted)
        {
            renderer.sprite = CorruptedSprite;
        }
        else
        {
            if (CurrentValue < 0)
                CurrentValue = 0;

            int index = (int)(((float)CurrentValue / (float)Max) * sprites.GetLength(0));
            if (index >= sprites.GetLength(0))
                index = sprites.GetLength(0) - 1;

            renderer.sprite = sprites[index];
        }
    }

    void FixedUpdate()
    {

    }
}
