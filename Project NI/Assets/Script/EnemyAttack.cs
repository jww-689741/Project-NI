using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform player;
    public float repeaterInterval;
    public float enemyShotSpeed;

    private Vector3[] shotPoint;

    // Start is called before the first frame update
    void Start()
    {
        shotPoint = new Vector3[2];
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("Repeater", "DirectBullet");
    }

    IEnumerator Repeater(string name)
    {
        if (this.gameObject.name == "DirectBullet")
        {
            while (true)
            {
                SetBullet(ObjectManager.instance.GetBullet(name), name); // źȯ �߻�
                yield return new WaitForSeconds(repeaterInterval); // ����ð���ŭ ���
            }
        }
    }

    // źȯ ������Ʈ ��ġ ����, ȸ������ ����, ������Ʈ Ȱ��ȭ, ���� �߻� ���� �۵�
    private void SetBullet(GameObject bullet, string name)
    {
        if (bullet == null) return; // �޾ƿ� źȯ�� ���� ��� ��ȯ

        Vector3 bulletPosition = new Vector3(this.transform.position.x, this.transform.position.y, (this.transform.position.z - 1.5f));

        shotPoint[0] = player.position; // ��ǥ����, �÷��̾�
        shotPoint[1] = bulletPosition; // ��������, ��

        bullet.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, (this.transform.position.z - 1.5f)); // ��ġ ����
        bullet.transform.rotation = this.transform.rotation; // ȸ���� ����
        bullet.SetActive(true); // Ȱ��ȭ

        if (name == "DirectBullet")
        {
            bullet.GetComponent<BulletManager>().StartCoroutine("Shot", shotPoint); // źȯ ���� ���� �ڷ�ƾ ����
        }
        else if (name == "HowitzerBullet")
        {
            bullet.GetComponent<BulletManager>().StartCoroutine("Shot", shotPoint); // źȯ ���� ���� �ڷ�ƾ ����
        }
        else if (name == "Buckshot")
        {
            for (int i = 0; i < 5; i++)
            {
                bullet.transform.GetChild(i).transform.position = bullet.transform.position;  //�ڽ� ������Ʈ �θ�� ������ ��ġ�� �̵�
            }
            bullet.GetComponent<BulletManager>().StartCoroutine("Shot", shotPoint); // źȯ ���� ���� �ڷ�ƾ ����
        }
    }

    // ��� ����
    private Vector3 CalculatingExtendedDistance(Vector3 startPoint, Vector3 endPoint, float distance)
    {
        float posZ = Mathf.Sqrt(Mathf.Pow(startPoint.z - endPoint.z, 2) + Mathf.Pow(startPoint.y - endPoint.y, 2));
        float posX = Mathf.Abs(startPoint.x - endPoint.x); // �� ���� x ����
        float posY = (distance * Mathf.Abs(startPoint.y - endPoint.y)) / Mathf.Abs(startPoint.z - endPoint.z);

        float targetX = (posX * distance) / posZ; // x, z������ ���
        float targetY = (posY * distance) / posZ; // y, z������ ���
        float targetZ = distance; // �Ű������� ���� �Ÿ�

        if(startPoint.x - endPoint.x < 0)
        {
            targetX *= -1;
        }
        else if (startPoint.y - endPoint.y < 0)
        {
            targetY *= -1;
        }
        else if (startPoint.z - endPoint.z < 0)
        {
            targetZ *= -1;
        }

        return new Vector3(targetX, targetY, targetZ);
    }
}
