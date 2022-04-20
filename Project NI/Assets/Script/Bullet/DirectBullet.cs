using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class DirectBullet : MonoBehaviour, IBulletShot
{

    // źȯ�� ���ݼӵ��� ��ȯ�ϴ� �޼ҵ� ������
    public float GetAttackSpeedToBullet()
    {
        var status = GetComponent<DirectBulletStatusManager>(); // ����źȯ�� ���� ������ ����
        return status.GetAttackSpeed(); // ���ݼӵ� �� ��ȯ
    }

    // �߻� �߻� �ڷ�ƾ ������
    public IEnumerator Shot(Vector3 directionVector)
    {
        var status = GetComponent<DirectBulletStatusManager>(); // ����źȯ�� ���� ������ ����
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer > status.GetHoldingTime()) break;

            transform.Translate(Vector3.forward * Time.deltaTime * status.GetShotSpeed()); // źȯ �߻�
            yield return null; // �ڷ�ƾ ������ ����
        }

        this.gameObject.SetActive(false); // ��Ȱ��ȭ
    }

}
