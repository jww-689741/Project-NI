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

<<<<<<< HEAD
<<<<<<< HEAD
    public IEnumerator Shot()
=======
    public IEnumerator Shot(Vector3 directionVector)
>>>>>>> origin/Pks
=======
    public IEnumerator Shot(Vector3 directionVector)
>>>>>>> origin/Jms
    {
        var status = GetComponent<DirectBulletStatusManager>(); // ����źȯ�� ���� ������ ����
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer > status.GetHoldingTime()) break;
            for (int i = 0; i < 5; i++)
            {
<<<<<<< HEAD
                transform.GetChild(i).gameObject.transform.Translate(Vector3.forward * Time.deltaTime * status.GetShotSpeed()); // ��ź źȯ �߻�
=======
                transform.GetChild(i).gameObject.transform.Translate(directionVector * Time.deltaTime * status.GetShotSpeed()); // ��ź źȯ �߻�
>>>>>>> origin/Jms
            }
            yield return null; // �ڷ�ƾ ������ ����
        }

        this.gameObject.SetActive(false); // ��Ȱ��ȭ
    }
<<<<<<< HEAD
<<<<<<< HEAD

    public float GetAttackDamageToBullet()
    {
        var status = GetComponent<DirectBulletStatusManager>(); // ����źȯ�� ���� ������ ����
        return status.GetAttackDamage(); // ���ݷ� �� ��ȯ
    }
=======
>>>>>>> origin/Pks
=======
>>>>>>> origin/Jms
}
