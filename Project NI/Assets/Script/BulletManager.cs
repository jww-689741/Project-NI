using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public float playerShotSpeed; // �÷��̾� �߻� �ӵ�
    public float satelliteShotSpeed; // ��Ʋ����Ʈ �߻� �ӵ�
    public float spinnerBulletSpeed; // ȸ��ź �߻�ӵ�
    public float spinnerBulletInterval = 0.1f; // ȸ��ź �߻� ����

    public float firingAngle = 45.0f;  //����
    public float gravity = 9.8f;  //�߷�
    public float weight = 1.3f;   //����ġ��
    
    private Transform m_tfTarget = null; //transform ����
    private float timeTemp;
    private float m_currentSpeed = 0f;   //�̻��� ����ӵ�
    [SerializeField] LayerMask m_layerMask = 0; //���ϴ� ���̾ �������ִ� LayerMask ����
    // �÷��̾��� �߻� �߻� �ڷ�ƾ
    IEnumerator Shot(Vector3[] shotVecter)
    {
        if (this.gameObject.name == "DirectBullet")
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
            yield return new WaitForSeconds(0.2f);   // 0.2�� ���
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
        }
        else if (this.gameObject.name == "SpinnerBullet")
        {
            float timer = 0;
            int spinnerBulletCount = 0;
            while (true)
            {
                timer += Time.deltaTime;
                if (timer < spinnerBulletInterval)
                {
                    this.gameObject.transform.Translate(Vector3.forward * Time.deltaTime * spinnerBulletSpeed);
                }
                else if (timer > 2) break;
                else
                {
                    this.gameObject.transform.Rotate(Vector3.up * Time.deltaTime * 30f);
                    if (timer > spinnerBulletInterval)
                    {
                        if (timer > spinnerBulletInterval * 1.5f && spinnerBulletCount < 9)
                        {
                            spinnerBulletCount++;
                        }
                        for (int i = 0; i < 4; i++)
                        {
                            this.gameObject.transform.GetChild(spinnerBulletCount).GetChild(i).Translate(Vector3.forward * Time.deltaTime * spinnerBulletSpeed);
                        }
                    }
                }
                yield return null; // �ڷ�ƾ ������ ����
            }
        }
        else if (this.gameObject.name == "ChaserBullet")
        {
            Vector3 directionVector = ((shotVecter[0] - shotVecter[1]).normalized); // (���콺 ��ǥ - �߻� �������� ��ǥ).���⺤��
            float timer = 0;
            SearchEnemy(); //�� Ž��
            while (true)
            {
                timer += Time.deltaTime;
                if (timer > 2) break;
                transform.Translate(directionVector * Time.deltaTime * playerShotSpeed);
                if (m_tfTarget != null)
                {
                    if (m_currentSpeed <= playerShotSpeed)  //���� �ӵ��� �ִ�ӵ� �����϶� �ִ�ӵ����� ����
                        m_currentSpeed += playerShotSpeed * Time.deltaTime;

                    Vector3 t_dir = (m_tfTarget.position - transform.position).normalized; //ǥ����ġ - �̻��� ��ġ = ����� �Ÿ� ���� normalized�� ���⸸ ����
                    transform.up = Vector3.Lerp(transform.up, t_dir, 0.25f);   //�̻��� y��(�Ӹ�)�� �ش� �������� ����
                    transform.Translate(transform.up * Time.deltaTime * m_currentSpeed);  //ǥ���� ������ �̻��� ���� ����
                }
                else
                {
                    transform.Translate(directionVector * Time.deltaTime * playerShotSpeed); // źȯ �߻�
                    SearchEnemy(); //�� Ž��
                }
                yield return null; // �ڷ�ƾ ������ ����
            }
        }
        this.gameObject.SetActive(false); // ��Ȱ��ȭ
    }

    // ��Ʋ����Ʈ�� �߻� �ڷ�ƾ

    IEnumerator SatelliteShot()
    {
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer > 2) break;

            transform.Translate(Vector3.forward * Time.deltaTime * satelliteShotSpeed); // źȯ �߻�
            yield return null; // �ڷ�ƾ ������ ����
        }

        gameObject.SetActive(false); // ��Ȱ��ȭ
    }

    // ��Ž�� �Լ�
    void SearchEnemy()  //ǥ�� Ž�� �Լ�
    {
        Collider[] t_cols = Physics.OverlapSphere(transform.position, 100f, m_layerMask);  //�ݰ� 100m���� Ư�� ���̾� �ö��̴� ����

        if (t_cols.Length > 0)  //����� �͵� �� �ϳ��� �������� ǥ�� ����
        {
            m_tfTarget = t_cols[Random.Range(0, t_cols.Length)].transform;
        }
    }
    private void OnCollisionEnter(Collision collision)  //�±װ� Enemy�� �ö��̴��� �浹�ϸ� �Ѵ� ����
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            gameObject.SetActive(false);
        }
    }
}
