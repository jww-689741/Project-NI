using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerChild : MonoBehaviour
{
    private void Update()
    {
        var status = GetComponentInParent<SpinnerBulletStausManager>(); // �θ��� ���� ������Ʈ�� ������
        var HoldingTime = 4; // ��ź�� �߻����
        for (int i = 0; i < HoldingTime; i++)
        {
            this.gameObject.transform.GetChild(i).Translate(Vector3.forward * Time.deltaTime * status.GetShotSpeed()); // ��ź �߻�
        }
    }
}
