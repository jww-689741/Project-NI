using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float repeaterInterval; // ���� �ӵ�
    
    private bool repeaterLock; // ���� ����

    private void Start()
    {
        repeaterLock = true; // ���� Ȱ��ȭ
    }

    private void Update()
    {
        Control(); // �ǽð� ���� ����
    }

    // �÷��̾� ����
    public void Control()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ��Ŭ�� �������� ��
        {
            StartCoroutine("Repeater");
        }
        else if (Input.GetMouseButtonUp(0)) repeaterLock = false; // ���콺 ��Ŭ������ ���� ����
    }

    // źȯ ���� �ڷ�ƾ
    IEnumerator Repeater()
    {
        repeaterLock = true; // ���� Ȱ��ȭ
        while (repeaterLock)
        {
            SetBulletField(ObjectManager.instance.GetBullet("Test")); // źȯ �߻�
            yield return new WaitForSeconds(repeaterInterval); // ����ð���ŭ ���
        }
    }

    // źȯ ������Ʈ ��ġ ����, ȸ������ ����, ������Ʈ Ȱ��ȭ
    void SetBulletField(GameObject bullet)
    {
        if (bullet == null) return; // �޾ƿ� źȯ�� ���� ��� ��ȯ

        bullet.transform.position = this.transform.position; // ��ġ ����
        bullet.transform.rotation = this.transform.rotation; // ȸ���� ����
        bullet.SetActive(true); // Ȱ��ȭ
        bullet.GetComponent<Bullet>().StartCoroutine("Shot"); // źȯ ���� ���� �ڷ�ƾ ����
    }

}
