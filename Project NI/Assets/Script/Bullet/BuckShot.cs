using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuckShot : MonoBehaviour, IBulletShot
{
    public float GetAttackSpeedToBullet()
    {
        var status = GetComponent<DirectBulletStatusManager>(); // ����źȯ�� ���� ������ ����
        return status.GetAttackSpeed(); // ���ݼӵ� �� ��ȯ
    }

    public IEnumerator Shot(Vector3 directionVector)
    {
        var status = GetComponent<DirectBulletStatusManager>(); // ����źȯ�� ���� ������ ����
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer > status.GetHoldingTime()) break;
            for (int i = 0; i < 5; i++)
            {
                transform.GetChild(i).gameObject.transform.Translate(directionVector * Time.deltaTime * status.GetShotSpeed()); // ��ź źȯ �߻�
            }
            yield return null; // �ڷ�ƾ ������ ����
        }

        this.gameObject.SetActive(false); // ��Ȱ��ȭ
    }
}
