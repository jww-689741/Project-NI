using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowitzerBullet : MonoBehaviour, IBulletShot
{
    public float GetAttackSpeedToBullet()
    {
        var status = GetComponent<HowitzerBulletStatusManager>(); // ���źȯ�� ���� ������ ����
        return status.GetAttackSpeed();
    }

    public IEnumerator Shot()
    {
        var status = GetComponent<HowitzerBulletStatusManager>(); // ���źȯ�� ���� ������ ����
        var gravity = -Physics.gravity.y; // �߷°�
        yield return new WaitForSeconds(0.01f);   // 0.2�� ���

        float target_Distance = 50f; // ���Ÿ�

        //������ �������� ��� ������Ʈ�� ������ �� �ʿ��� �ӵ��� ���
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * status.GetFiringAngle() * Mathf.Deg2Rad) / gravity);

        // �ӵ��� X Y ������Ʈ ����
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(status.GetFiringAngle() * Mathf.Deg2Rad) * status.GetWeight();
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(status.GetFiringAngle() * Mathf.Deg2Rad) * status.GetWeight();

        // ���� �ð��� ����մϴ�.
        float filghtDuration = target_Distance / Vx + status.GetWeight();

        // ���� �ð����� ������Ʈ Ȱ��ȭ
        float elapse_time = 0;
        while (elapse_time < filghtDuration)
        {
            transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);  //���������� źȯ �߻�
            elapse_time += Time.deltaTime;
            yield return null;
        }
        this.gameObject.SetActive(false); // ��Ȱ��ȭ
    }
}
