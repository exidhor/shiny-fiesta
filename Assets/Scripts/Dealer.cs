using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Dealer : MonoBehaviour
{
    public int Sell(int Amount, WeedType type)
    {
        int unitPrice = 0;

        switch (type)
        {
            case WeedType.Blue:
                unitPrice = 8;
                break;

            case WeedType.Purple:
                unitPrice = 2;
                break;
            
            case WeedType.Red:
                unitPrice = 50;
                break;
        }

        return unitPrice * Amount;
    }
}