using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerBullet : MonoBehaviour
{
    public float centerBulletSpeed; // �߾�ź �߻�ӵ�
    public float spinnerBulletInterval = 0.5f; // ȸ��ź �߻� ����

    IEnumerator Shot(bool direction)
    {
        float timer = 0; // ��ü ź ���ӽð���
        float timeTemp = 0; // ��ź �߻� ���ݿ�
        int spinnerBulletCount = 0; // ��ź �߻� ����
        for (int i = 0; i < 10; i++) // ��ź �ʱ� ��Ȱ��ȭ
        {
            this.gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
        while (true)
        {
            timer += Time.deltaTime;
            if (timer < 0.5f) // 0.5�ʵ��� �߾� ź ���� �߻�
            {
                this.gameObject.transform.Translate(Vector3.forward * Time.deltaTime * centerBulletSpeed);
            }
            else if (timer > 5) break; // ��ü �ð��� 5�ʰ� �Ѿ�� ���� �ƿ�
            else
            {
                if(direction) this.gameObject.transform.Rotate(Vector3.up * Time.deltaTime * 30f); // ������ ture���� �� �ð� �ݴ���� ȸ��
                else this.gameObject.transform.Rotate(Vector3.up * Time.deltaTime * -30f); // ������ false���� �� �ð���� ȸ��
                timeTemp += Time.deltaTime;
                if (timeTemp > spinnerBulletInterval) // ��ź Ȱ��ȭ
                {
                    timeTemp = 0;
                    this.gameObject.transform.GetChild(spinnerBulletCount).gameObject.SetActive(true);
                    if (spinnerBulletCount < 9) spinnerBulletCount++;
                }
            }
            yield return null; // �ڷ�ƾ ������ ����
        }
        this.gameObject.SetActive(false); // ��Ȱ��ȭ
    }
}
