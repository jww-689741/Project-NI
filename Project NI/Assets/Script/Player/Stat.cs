using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{

    // ½ºÅÈ
    private float divisionValue;
    private float ratio;
    public float maxValue { get; set; }
    public float currentValue
    {
        get { return divisionValue; }
        set
        {
            if (value > maxValue) divisionValue = maxValue;
            else if (value < 0) divisionValue = 0;
            else divisionValue = value;

            ratio = divisionValue / maxValue;
        }
    }

    public void SetDefaultStat(float current,float max)
    {
        maxValue = max;
        currentValue = current;
        Debug.Log(currentValue);
        Debug.Log(maxValue);
    }

    public float GetRatio()
    {
        return ratio;
    }
}
