using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD
<<<<<<< HEAD
=======
using System.Runtime.InteropServices;
>>>>>>> origin/Pks
=======
using System.Runtime.InteropServices;
>>>>>>> origin/Jms
using UnityEngine;

public class DirectBullet : MonoBehaviour, IBulletShot
{

    // źȯ�� ���ݼӵ��� ��ȯ�ϴ� �޼ҵ� ������
    public float GetAttackSpeedToBullet()
    {
        var status = GetComponent<DirectBulletStatusManager>(); // ����źȯ�� ���� ������ ����
        return status.GetAttackSpeed(); // ���ݼӵ� �� ��ȯ
    }

<<<<<<< HEAD
<<<<<<< HEAD
    public float GetAttackDamageToBullet()
    {
        var status = GetComponent<DirectBulletStatusManager>(); // ����źȯ�� ���� ������ ����
        return status.GetAttackDamage(); // ���ݷ� �� ��ȯ
    }

    // �߻� �߻� �ڷ�ƾ ������
    public IEnumerator Shot()
=======
    // �߻� �߻� �ڷ�ƾ ������
    public IEnumerator Shot(Vector3 directionVector)
>>>>>>> origin/Pks
=======
    // �߻� �߻� �ڷ�ƾ ������
    public IEnumerator Shot(Vector3 directionVector)
>>>>>>> origin/Jms
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
