using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public enum Type { Heart, Barrier, Bullet, Missile};
    public Type type;
    public int value;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(Vector3.up * 10 * Time.deltaTime);
    }
}
