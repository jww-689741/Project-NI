using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteManager : MonoBehaviour
{
    public GameObject prefab;
    public float repeaterInterval; // ����ӵ�
    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > repeaterInterval)
        {
            timer = 0;
            SetBullet(ObjectManager.instance.GetBullet("DirectBullet")); // źȯ �߻�
        }
    }

    // źȯ ������Ʈ ��ġ ����, ȸ������ ����, ������Ʈ Ȱ��ȭ, ���� �߻� ���� �۵�
    private void SetBullet(GameObject bullet)
    {
        if (bullet == null) return; // �޾ƿ� źȯ�� ���� ��� ��ȯ

        bullet.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, (this.transform.position.z + 0.6f)); // ��ġ ����
        bullet.transform.rotation = this.transform.rotation; // ȸ���� ����
        bullet.SetActive(true); // Ȱ��ȭ
        bullet.GetComponent<DirectBullet>().StartCoroutine("Shot",Vector3.forward); // źȯ ���� ���� �ڷ�ƾ ����
    }
}
