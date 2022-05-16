using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBomb : MonoBehaviour
{
    Rigidbody m_rigid = null;         //rigidbody ����
    Transform []m_tfTarget;      //transform ����

    private float m_currentSpeed = 0f;           //�̻��� ����ӵ�
    [SerializeField] LayerMask m_layerMask = 0; //���ϴ� ���̾ �������ִ� LayerMask ����

    void SearchEnemy()  //ǥ�� Ž�� �Լ�
    {
        Collider[] t_cols = Physics.OverlapSphere(transform.position, 500f, m_layerMask);  //�ݰ� 100m���� Ư�� ���̾� �ö��̴� ����
        m_tfTarget = new Transform[t_cols.Length];
        if(t_cols.Length > 0)  //����� �͵� �� �ϳ��� �������� ǥ�� ����
        {
            for(int i=0;i<t_cols.Length;i++)
            {
                m_tfTarget[i] = t_cols[i].transform;
            }    
        }
    }

    public IEnumerator Launch(int count) //�ڷ�ƾ
    {
        var status = GetComponent<MissileStatusManager>(); // ����źȯ�� ���� ������ ����
        float timer = 0;
        SearchEnemy(); //�� Ž��
        while (true)
        {
            timer += Time.deltaTime;
            if (timer > 5) break;
       
            transform.Translate(transform.forward * Time.deltaTime * status.GetShotSpeed());
            if (m_tfTarget != null)
            {
                if (m_currentSpeed <= status.GetShotSpeed())  //���� �ӵ��� �ִ�ӵ� �����϶� �ִ�ӵ����� ����
                    m_currentSpeed += status.GetShotSpeed() * Time.deltaTime;

                Vector3 t_dir = (m_tfTarget[count-1].position - transform.position).normalized; //ǥ����ġ - �̻��� ��ġ = ����� �Ÿ� ���� normalized�� ���⸸ ����
                transform.forward = Vector3.Lerp(transform.forward, t_dir, 0.25f);   //�̻��� y��(�Ӹ�)�� �ش� �������� ����
                transform.Translate(transform.forward * Time.deltaTime * m_currentSpeed);  //ǥ���� ������ �̻��� ���� ����
            }
            else
            {
                SearchEnemy();
                //transform.Translate(transform.forward * Time.deltaTime * status.GetShotSpeed());
                //SearchEnemy(); //�� Ž��
            }
            yield return null; // �ڷ�ƾ ������ ����
        }

        this.gameObject.SetActive(false); // ��Ȱ��ȭ

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            //other.gameObject.SetActive(false);
            //Destroy(collision.gameObject);
            gameObject.SetActive(false);
        }
    }
}
