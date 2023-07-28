using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CounterTaquin : MonoBehaviour
{

    public GameObject counterLeft, counterRight;

    public void RotateCounter(int displayedNumber)
    {
        int[] digits = displayedNumber.ToString().Select(t=>int.Parse(t.ToString())).ToArray();
        
        if (digits.Length > 1)
        {
            counterLeft.transform.rotation = Quaternion.Euler(0,180, ((360 / 10) * (digits[0] + 1 )));
            counterRight.transform.rotation = Quaternion.Euler(0,180, ((360 / 10) * (digits[1] + 1 )));
        }
        else
        {
            counterLeft.transform.rotation = Quaternion.Euler(0,180, ((360 / 10)));
            counterRight.transform.rotation = Quaternion.Euler(0,180, ((360 / 10) * (digits[0] + 1 )));
        }

    }
}