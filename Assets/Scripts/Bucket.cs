﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour {

    public int FillLevel = 0;
    public int CompleteLevel = 100;
    public ProgressSeau progress;
    public bool IsCorupted = false;

    public void Fill(int quantity)
    {
        if (quantity < 0)
        {
            IsCorupted = true;
            progress.IsCorrupted = true;
        }
        if (!IsCorupted)
        {
            FillLevel += quantity;
            if (FillLevel >= CompleteLevel)
            {
                FillLevel = CompleteLevel;
            }
        }
    }

    public bool IsFull()
    {
        return FillLevel == CompleteLevel;
    }

	// Use this for initialization
	void Start () {
        FillLevel = 0;
	}
	
	// Update is called once per frame
	void Update () {
        progress.CurrentValue = FillLevel;
	}
}
