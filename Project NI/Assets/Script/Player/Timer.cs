using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private void FixedUpdate()
    {
        
    }

    public float GetCurrentTime()
    {
        return Time.deltaTime;
    }
}
