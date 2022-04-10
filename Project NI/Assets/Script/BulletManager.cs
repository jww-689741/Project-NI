using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public float playerShotSpeed; // �߻� �ӵ�
    public float SatelliteShotSpeed; // �߻� �ӵ�

    public float firingAngle = 45.0f;  //����
    public float gravity = 9.8f;  //�߷�
    public float weight = 1.3f;   //����ġ��

    // �÷��̾��� �߻� �߻� �ڷ�ƾ
    IEnumerator Shot(Vector3[] shotVecter)
    {
        if(this.gameObject.name == "DirectBullet")
        {
            Vector3 directionVector = ((shotVecter[0] - shotVecter[1]).normalized); // (���콺 ��ǥ - �߻� �������� ��ǥ).���⺤��
            float timer = 0;
            while (true)
            {
                timer += Time.deltaTime;
                if (timer > 2) break;

                transform.Translate(directionVector * Time.deltaTime * playerShotSpeed); // źȯ �߻�
                yield return null; // �ڷ�ƾ ������ ����
            }
        }
        else if (this.gameObject.name == "HowitzerBullet")
        {
            yield return new WaitForSeconds(0.5f);   // 0.5�� ���
                                                     // ���Ÿ�
            float target_Distance = 20f;

            //������ �������� ��� ������Ʈ�� ������ �� �ʿ��� �ӵ��� ���
            float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

            // �ӵ��� X Y ������Ʈ ����
            float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad) * weight;
            float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad) * weight;

            // ���� �ð��� ����մϴ�.
            float filghtDuration = target_Distance / Vx + weight;

            // ���� �ð����� ������Ʈ Ȱ��ȭ
            float elapse_time = 0;
            while (elapse_time < filghtDuration)
            {
                transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);  //���������� źȯ �߻�
                elapse_time += Time.deltaTime;
                yield return null;
            }
            gameObject.SetActive(false); // ��Ȱ��ȭ
        }
        else if (this.gameObject.name == "Buckshot")
        {
            Vector3 directionVector = ((shotVecter[0] - shotVecter[1]).normalized); // (���콺 ��ǥ - �߻� �������� ��ǥ).���⺤��
            float timer = 0;
            while (true)
            {
                timer += Time.deltaTime;
                if (timer > 2) break;
                for (int i = 0; i < 5; i++)
                {
                    this.transform.GetChild(i).gameObject.transform.Translate(directionVector * Time.deltaTime * playerShotSpeed); // źȯ �߻�
                }
                yield return null; // �ڷ�ƾ ������ ����
            }

            gameObject.SetActive(false); // ��Ȱ��ȭ
        }

        gameObject.SetActive(false); // ��Ȱ��ȭ
    }

    // ��Ʋ����Ʈ�� �߻� �ڷ�ƾ

    IEnumerator SatelliteShot()
    {
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer > 2) break;

            transform.Translate(Vector3.forward * Time.deltaTime * SatelliteShotSpeed); // źȯ �߻�
            yield return null; // �ڷ�ƾ ������ ����
        }

        gameObject.SetActive(false); // ��Ȱ��ȭ
    }
}
