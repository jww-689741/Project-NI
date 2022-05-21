using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ChaserBullet : MonoBehaviour
{
    [SerializeField] LayerMask m_layerMask = 0; //���ϴ� ���̾ �������ִ� LayerMask ����
    private Transform m_tfTarget = null; //transform ����
    private float m_currentSpeed = 0f;   //�̻��� ����ӵ�

    public float GetAttackDamageToBullet()
    {
        var status = GetComponent<ChaserBulletStatusManager>(); // źȯ�� ���� ������ ����
        return status.GetAttackDamage();
    }

    // �߻� �߻� �ڷ�ƾ
    public IEnumerator Shot(Vector3 directionVector)
    {
        var status = GetComponent<ChaserBulletStatusManager>(); // ����źȯ�� ���� ������ ����
        float timer = 0;
        //SearchEnemy(); //�� Ž��
        while (true)
        {
            timer += Time.deltaTime;
            if (timer > status.GetHoldingTime()) break;
            transform.forward = Vector3.Lerp(transform.forward, directionVector, 0.25f);
            transform.Translate(transform.forward * Time.deltaTime * status.GetShotSpeed());
            if (m_tfTarget != null)
            {
                if (m_currentSpeed <= status.GetShotSpeed())  //���� �ӵ��� �ִ�ӵ� �����϶� �ִ�ӵ����� ����
                    m_currentSpeed += status.GetShotSpeed() * Time.deltaTime;

                Vector3 t_dir = (m_tfTarget.position - transform.position).normalized; //ǥ����ġ - �̻��� ��ġ = ����� �Ÿ� ���� normalized�� ���⸸ ����
                transform.forward = Vector3.Lerp(transform.forward, t_dir, 0.25f);   //�̻��� y��(�Ӹ�)�� �ش� �������� ����
                transform.Translate(transform.forward * Time.deltaTime * m_currentSpeed);  //ǥ���� ������ �̻��� ���� ����
            }
            else
            {
                transform.Translate(directionVector * Time.deltaTime * status.GetShotSpeed()); // źȯ �߻�
                SearchEnemy(); //�� Ž��
            }
            yield return null; // �ڷ�ƾ ������ ����
        }

        this.gameObject.SetActive(false); // ��Ȱ��ȭ
    }
    void SearchEnemy()  //ǥ�� Ž�� �Լ�
    {
        Collider[] t_cols = Physics.OverlapSphere(transform.position, 50f, m_layerMask);  //�ݰ� 50m���� Ư�� ���̾� �ö��̴� ����

        if (t_cols.Length > 0)  //����� �͵� �� �ϳ��� �������� ǥ�� ����
        {
            m_tfTarget = t_cols[Random.Range(0, t_cols.Length)].transform;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            // Destroy(other.gameObject);
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
