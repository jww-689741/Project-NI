using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    Rigidbody m_rigid = null;         //rigidbody ����
    Transform m_tfTarget = null;      //transform ����

    [SerializeField] float m_speed = 0f; //�̻��� �ְ�ӵ�
    float m_currentSpeed = 0f;           //�̻��� ����ӵ�
    [SerializeField] LayerMask m_layerMask = 0; //���ϴ� ���̾ �������ִ� LayerMask ����
    [SerializeField] ParticleSystem m_psEffect = null; //�̻��� ��ƼŬ �ý��� ����

    void SearchEnemy()  //ǥ�� Ž�� �Լ�
    {
        Collider[] t_cols = Physics.OverlapSphere(transform.position, 100f, m_layerMask);  //�ݰ� 100m���� Ư�� ���̾� �ö��̴� ����

        if(t_cols.Length > 0)  //����� �͵� �� �ϳ��� �������� ǥ�� ����
        {
            m_tfTarget = t_cols[Random.Range(0, t_cols.Length)].transform;
        }
    }

    IEnumerator LaunchDelay() //�ڷ�ƾ
    {
        yield return new WaitUntil(() => m_rigid.velocity.y < 0f);  //velocity�� y���� 0���ϰ� �� ������ ���
        //yield return new WaitForSeconds(0.1f);  //0.1�� ���

        SearchEnemy(); //�� Ž��
        m_psEffect.Play();  //��ƼŬ ����

        yield return new WaitForSeconds(5f);   //5�ʰ� ������ �ƹ��� ������ �̻��� �ı�
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        m_rigid = GetComponent<Rigidbody>();
        StartCoroutine(LaunchDelay());
    }

    // Update is called once per frame
    void Update()
    {
        if(m_tfTarget != null)
        {
            if (m_currentSpeed <= m_speed)  //���� �ӵ��� �ִ�ӵ� �����϶� �ִ�ӵ����� ����
                m_currentSpeed += m_speed * Time.deltaTime;

            transform.position += transform.up * m_currentSpeed * Time.deltaTime;  //ǥ���� ������ �̻��� ���� ����

            Vector3 t_dir = (m_tfTarget.position - transform.position).normalized; //ǥ����ġ - �̻��� ��ġ = ����� �Ÿ� ���� normalized�� ���⸸ ����
            transform.up = Vector3.Lerp(transform.up, t_dir, 0.25f);   //�̻��� y��(�Ӹ�)�� �ش� �������� ����
        }
    }


    private void OnCollisionEnter(Collision collision)  //�±װ� Enemy�� �ö��̴��� �浹�ϸ� �Ѵ� ����
    {
        if(collision.transform.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
