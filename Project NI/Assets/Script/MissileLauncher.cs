using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    [SerializeField] GameObject m_goMissile = null;        //�̻��� ������ ���� ����
    [SerializeField] Transform m_tfMissileSpawn = null;    //�߻�� ��ġ ���� ����

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))                //�����̽��� -> �̻��� ���� -> ���� �߻�
        {
            GameObject t_missile = Instantiate(m_goMissile, m_tfMissileSpawn.position, Quaternion.identity);
            t_missile.GetComponent<Rigidbody>().velocity = Vector3.up * 5f;
        }
    }
}
