using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerChild : MonoBehaviour
{
    public float speed = 50; // 자탄 발사 속도

    private void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            this.gameObject.transform.GetChild(i).Translate(Vector3.forward * Time.deltaTime * speed); // 자탄 발사
        }
    }
}
