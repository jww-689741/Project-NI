using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerBullet : MonoBehaviour, IBulletShot
{
    public IEnumerator Shot(Vector3 directionVector)
    {
        var status = GetComponent<SpinnerBulletStausManager>();
        var timer = 0f; // ��ü ź ���ӽð���
        var timeTemp = 0f; // ��ź �߻� ���ݿ�
        var count = 0; // ��ź �� ī��Ʈ
        var bulletTransform = this.gameObject.transform; // �ڱ� �ڽ��� Transform
        for (int i = 0; i < status.GetChildBulletNumber(); i++) // ��ź �ʱ� ��Ȱ��ȭ
        {
            bulletTransform.GetChild(i).gameObject.SetActive(false);
        }
        while (true)
        {
            timer += Time.deltaTime;
            if (timer < status.GetAttackSpeed()) // 0.5�ʵ��� �߾� ź ���� �߻�
            {
                bulletTransform.Translate(Vector3.forward * Time.deltaTime * status.GetShotSpeed());
            }
            else if (timer > status.GetHoldingTime()) break; // �����ð��� �Ѿ�� ���� �ƿ�
            else
            {
                if (status.GetRotateDirection()) bulletTransform.Rotate(Vector3.up * Time.deltaTime * -status.GetRotateAngle()); // �ð� ���� ȸ��
                else bulletTransform.Rotate(Vector3.up * Time.deltaTime * status.GetRotateAngle()); // �ð� �ݴ� ���� ȸ��
                timeTemp += Time.deltaTime;
                if (timeTemp > status.GetAttackSpeed()) // ��ź Ȱ��ȭ
                {
                    timeTemp = 0;
                    bulletTransform.GetChild(count).gameObject.SetActive(true);
                    if (count < status.GetChildBulletNumber()) ++count;
                }
            }
            yield return null; // �ڷ�ƾ ������ ����
        }
        this.gameObject.SetActive(false); // ��Ȱ��ȭ
    }

    public float GetAttackSpeedToBullet()
    {
        var status = GetComponent<SpinnerBulletStausManager>();
        return status.GetAttackSpeed();
    }
}
