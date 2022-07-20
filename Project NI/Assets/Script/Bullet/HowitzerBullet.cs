using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowitzerBullet : MonoBehaviour, IBulletShot
{
<<<<<<< HEAD
<<<<<<< HEAD
=======
    public GameObject explosion; // �ı� ȿ��
>>>>>>> origin/Pks
=======
>>>>>>> origin/Jms
    public float GetAttackSpeedToBullet()
    {
        var status = GetComponent<HowitzerBulletStatusManager>(); // ���źȯ�� ���� ������ ����
        return status.GetAttackSpeed();
    }

<<<<<<< HEAD
<<<<<<< HEAD
    public IEnumerator Shot()
=======
    public IEnumerator Shot(Vector3 directionVector)
>>>>>>> origin/Jms
    {
        var status = GetComponent<HowitzerBulletStatusManager>(); // ���źȯ�� ���� ������ ����
        var gravity = -Physics.gravity.y; // �߷°�
        yield return new WaitForSeconds(0.01f);   // 0.2�� ���

        float target_Distance = 50f; // ���Ÿ�
<<<<<<< HEAD
=======
    public IEnumerator Shot(Vector3 directionVector)
    {
        var status = GetComponent<HowitzerBulletStatusManager>(); // ���źȯ�� ���� ������ ����
        var gravity = -Physics.gravity.y; // �߷°�
        //yield return new WaitForSeconds(0.01f);   // 0.2�� ���

        float target_Distance = 30f; // ���Ÿ�
>>>>>>> origin/Pks
=======
>>>>>>> origin/Jms

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
<<<<<<< HEAD

<<<<<<< HEAD
    public float GetAttackDamageToBullet()
    {
        var status = GetComponent<HowitzerBulletStatusManager>(); // ���źȯ�� ���� ������ ����
        return status.GetAttackDamage();
    }
=======
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Instantiate(explosion, this.transform.position, this.transform.rotation);
            this.gameObject.SetActive(false);
            //explosion.SetActive(false);
        }
    }

>>>>>>> origin/Pks
=======
>>>>>>> origin/Jms
}
