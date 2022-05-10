using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    [SerializeField] GameObject m_goMissile = null;        //�̻��� ������ ���� ����
    [SerializeField] Transform m_tfMissileSpawn = null;    //�߻�� ��ġ ���� ����
    [SerializeField] LayerMask m_layerMask = 0; //���ϴ� ���̾ �������ִ� LayerMask ����
    private int i = 0;
    float time = 0;
    float coolTime = 0;
    int count = 0;
    int cart = 3; // ��ź ��
    bool flag = false;
    bool coolFlag = false;
    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && cart > 0 && !coolFlag)                //�����̽��� -> �̻��� ���� -> ���� �߻�
        {
            count = SearchEnemy();
            flag = true;
            coolFlag = true;
            cart--;
        }
        if (coolFlag)
        {
            coolTime += Time.deltaTime;
            Debug.Log(coolTime);
            if (coolTime >= 1)
            {
                coolFlag = false;
                coolTime = 0;

            }
        }
        if(flag && time>0.1f)
        {
                SetMissile(ObjectManager.instance.GetBullet("MissileBomb"),count);
                time = 0;
                count--;

                if (count == 0) flag = false;
          
        }
    
    }

    public int GetCart()
    {
        return this.cart;
    }

    private void SetMissile(GameObject Missile, int count)
    {
        var bulletTf = Missile.transform; // źȯ�� transform��
        var playerTf = this.transform; // �÷��̾��� transform��
        bulletTf.position = new Vector3(playerTf.position.x, playerTf.position.y, playerTf.position.z+1); // ��ġ ����

        if (Missile == null) return; // �޾ƿ� źȯ�� ���� ��� ��ȯ
        Missile.SetActive(true); // Ȱ��ȭ
        Missile.GetComponent<MissileBomb>().StartCoroutine("Launch",count);
        
        
    }
    int SearchEnemy()  //ǥ�� Ž�� �Լ�
    {
        Collider[] t_cols = Physics.OverlapSphere(transform.position, 500f, m_layerMask);  //�ݰ� 100m���� Ư�� ���̾� �ö��̴� ����
        Debug.Log(t_cols.Length);

       if (t_cols.Length > 0)
        {
            return t_cols.Length;
        }
        else
        {
            return 0;
        }
    }
}
