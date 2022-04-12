using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerChild : MonoBehaviour
{
    public float speed = 50; // ��ź �߻� �ӵ�

    private void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            this.gameObject.transform.GetChild(i).Translate(Vector3.forward * Time.deltaTime * speed); // ��ź �߻�
        }
    }
}
