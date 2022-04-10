using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float repeaterInterval; // ����ӵ�
    private bool repeaterLock; // ���� ����
    public float degree; //ȸ�� ����
    private delegate void Control();
    Control control;
    void Start()
    {
        repeaterLock = true;
        control = Shot;
    }

    // Update is called once per frame
    void Update()
    {
        control();
        transform.Rotate(Vector3.up * Time.deltaTime * degree);
    }
    private void Shot()
    {
       /* if (Input.GetMouseButtonDown(1)) // ���콺 ��Ŭ�� �������� ��
        {
            StartCoroutine("Repeater1");
        }
        else if (Input.GetMouseButtonUp(1)) repeaterLock = false; // ���콺 ��Ŭ������ ���� ����*/
        StartCoroutine("Repeater");
    }

    IEnumerator Repeater()
    {
        repeaterLock = true; // ���� Ȱ��ȭ
        while (repeaterLock)
        {
            for(int i= 0; i<4;i++)
            {
                SetBullet(ObjectManager.instance.GetBullet("Test"),i); // źȯ �߻�
                //yield return new WaitForSeconds(repeaterInterval);
            }
            yield return new WaitForSeconds(repeaterInterval); // ����ð���ŭ ���
        }
    }
    // źȯ ������Ʈ ��ġ ����, ȸ������ ����, ������Ʈ Ȱ��ȭ, ���� �߻� ���� �۵�
    private void SetBullet(GameObject bullet,int direction)
    {
        if (bullet == null) return; // �޾ƿ� źȯ�� ���� ��� ��ȯ
        if (direction == 0)  // ���� �߻�
        {
            bullet.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, (this.transform.position.z + 1.5f)); // ��ġ ����
        }
        else if (direction == 1) //���� �߻�
        {
            bullet.transform.position = new Vector3(this.transform.position.x - 1.5f, this.transform.position.y, (this.transform.position.z)); // ��ġ ����
        }
        else if (direction == 2) //���� �߻�
        {
            bullet.transform.position = new Vector3(this.transform.position.x + 1.5f, this.transform.position.y, (this.transform.position.z)); // ��ġ ����
        }
        else if (direction == 3) //�ĸ� �߻�
        {
            bullet.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, (this.transform.position.z - 1.5f)); // ��ġ ����
        }
        bullet.transform.rotation = this.transform.rotation; // ȸ���� ����
        bullet.SetActive(true); // Ȱ��ȭ
        bullet.GetComponent<Bullet4>().StartCoroutine("Shot",direction); // źȯ ���� ���� �ڷ�ƾ ����
    }
    
}