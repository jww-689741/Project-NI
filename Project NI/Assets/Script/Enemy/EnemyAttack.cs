using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float repeaterInterval = 2f;

    private float timer = 0; // �� ���� ���� �ִ� Ÿ�̸�
    private Transform enemyTransform; // �ڱ� �ڽ��� ��ǥ�� ����
    private Transform player; // �÷��̾� ��ǥ�� �������� ���� ������Ʈ

    private void Awake()
    {
        enemyTransform = this.gameObject.transform;
        player = GameObject.FindWithTag("Player").transform;
    }

    void Start()
    {

    }

    void Update()
    {
        if (enemyTransform.position.z > player.position.z + 10) // ���� �÷��̾� ���� ���濡 ���� ���� ���� ����
        {
            if (timer > repeaterInterval)
            {
                StartCoroutine("Repeater", "DirectBullet");
                timer = 0;
            }
            timer += Time.deltaTime;
        }
    }

    IEnumerator Repeater(string name)
    {
        SetBullet(ObjectManager.instance.GetBullet(name), name); // źȯ �߻�
        yield return new WaitForSeconds(repeaterInterval); // ����ð���ŭ ���
    }

    // źȯ ������Ʈ ��ġ ����, ȸ������ ����, ������Ʈ Ȱ��ȭ, ���� �߻� ���� �۵�
    private void SetBullet(GameObject bullet, string name)
    {
        if (bullet == null) return; // �޾ƿ� źȯ�� ���� ��� ��ȯ

        Vector3 bulletPosition = new Vector3(this.transform.position.x, this.transform.position.y, (this.transform.position.z - 1.5f));

        bullet.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, (this.transform.position.z - 1.5f)); // ��ġ ����
        var directionVector = (player.position - bulletPosition).normalized; // źȯ�� �߻� �� ���⺤�� ����
        bullet.transform.localRotation = Quaternion.LookRotation(directionVector);
        bullet.SetActive(true); // Ȱ��ȭ

        if (name == "DirectBullet")
        {
            bullet.GetComponent<DirectBullet>().StartCoroutine("Shot", directionVector); // źȯ ���� ���� �ڷ�ƾ ����
        }
        else if (name == "HowitzerBullet")
        {
            bullet.GetComponent<HowitzerBullet>().StartCoroutine("Shot", directionVector); // źȯ ���� ���� �ڷ�ƾ ����
        }
        else if (name == "Buckshot")
        {
            for (int i = 0; i < 5; i++)
            {
                bullet.transform.GetChild(i).transform.position = bullet.transform.position;  //�ڽ� ������Ʈ �θ�� ������ ��ġ�� �̵�
            }
            bullet.GetComponent<BuckShot>().StartCoroutine("Shot", directionVector); // źȯ ���� ���� �ڷ�ƾ ����
        }
    }
}
