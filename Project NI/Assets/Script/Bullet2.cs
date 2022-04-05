using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    public float firingAngle = 45.0f;  //����
    public float gravity = 9.8f;  //�߷�
    public float weight = 1.3f;   //����ġ��
    // źȯ �߻� �ڷ�ƾ
    IEnumerator Shot(Vector3[] shotVecter)
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
        while(elapse_time < filghtDuration)
        {
            transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);  //���������� źȯ �߻�
            elapse_time += Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false); // ��Ȱ��ȭ
    }
}
