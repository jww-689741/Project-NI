using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestObjectManager : MonoBehaviour
{
    // �̱���
    public static TestObjectManager instance;

    // ���� �� �θ� ������Ʈ ����
    [Header("Parent Object")]
    [SerializeField]
    private Transform BulletPool;

    [Header("Parent Object")]
    [SerializeField]
    private Transform EnemyPool;

    // �̱��� ����
    private void Awake()
    {
        instance = this;
    }

    // ���� ���� �� �޸� �Ҵ�����
    private void OnDestroy()
    {
        MemoryClear();
    }

    public BulletInfo[] bulletinfo = null; // źȯ ������Ʈ ������ �迭
    public List<GameObject> bulletList; // ������ źȯ ����Ʈ

    public EnemyInfo[] enemyinfo = null; // �� ������Ʈ ������ �迭
    public List<GameObject> enemyList; // ������ �� ����Ʈ

    // ������Ʈ ���� ����
    private void Start()
    {
        // źȯ ����
        if (bulletinfo.Length < 1) SetBullet(bulletinfo[0].prefab, bulletinfo[0].count, bulletinfo[0].name); // ����Ʈ�� ������ �ϳ��� ���
        else // ����Ʈ�� ������ �� �̻��� ���
        {
            for (int i = 0; i < bulletinfo.Length; i++)
            {
                SetBullet(bulletinfo[i].prefab, bulletinfo[i].count, bulletinfo[i].name);
            }
        }

        // �� ����
        if (enemyinfo.Length < 1) SetEnemy(enemyinfo[0].prefab, enemyinfo[0].count, enemyinfo[0].name); // ����Ʈ�� ������ �ϳ��� ���
        else // ����Ʈ�� ������ �� �̻��� ���
        {
            for (int i = 0; i < enemyinfo.Length; i++)
            {
                SetEnemy(enemyinfo[i].prefab, enemyinfo[i].count, enemyinfo[i].name);
            }
        }
    }

    // źȯ ����, �Ӽ� �Է�, ����Ʈ ����
    public void SetBullet(GameObject bullet, int count, string name)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject clone = Instantiate(bullet) as GameObject; // Ŭ�� ����
            clone.transform.name = name; // �̸�
            clone.transform.localPosition = Vector3.zero; // ��ġ
            clone.SetActive(false); // ��Ȱ��ȭ
            clone.transform.parent = BulletPool; // �θ� ������Ʈ ����
            bulletList.Add(clone); // ����Ʈ ����
        }
    }

    // �� ����, �Ӽ� �Է�, ����Ʈ ����
    public void SetEnemy(GameObject enemy, int count, string name)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject clone = Instantiate(enemy) as GameObject; // Ŭ�� ����
            clone.transform.name = name; // �̸�
            clone.transform.localPosition = Vector3.zero; // ��ġ
            clone.SetActive(false); // ��Ȱ��ȭ
            clone.transform.parent = EnemyPool; // �θ� ������Ʈ ����
            enemyList.Add(clone); // ����Ʈ ����
        }
    }

    // źȯ ȣ��
    public GameObject GetBullet(string name)
    {
        if (bulletList == null) return null; // ����Ʈ�� ��������� null ��ȯ

        int listCount = bulletList.Count;
        for (int i = 0; i < listCount; i++)
        {
            if (name != bulletList[i].name) continue; // ã�� �̸��� źȯ�� ������ �Ѿ

            GameObject targetBullet = bulletList[i]; // ã�� źȯ�� ������Ʈ

            if (targetBullet.activeSelf == true) // źȯ�� Ȱ��ȭ ��
            {
                if (i == listCount - 1) // ����Ʈ �� ��� źȯ�� Ȱ��ȭ ������ ��
                {
                    SetBullet(targetBullet, 1, targetBullet.name); // ���ο� źȯ 1�� �߰�
                    return bulletList[i + 1]; // ���� ������ źȯ ��ȯ
                }
                continue;
            }
            return bulletList[i]; // ��Ȱ��ȭ ������ źȯ ��ȯ
        }
        return null; // ȣ�� �� źȯ�� �̸��� ã�� ���� ��� null��ȯ
    }

    // �� ���� ȣ��
    public GameObject[] GetEnemy(string name, int count)
    {
        GameObject[] target = new GameObject[count]; // ������ �� ������Ʈ ����� �迭
        GameObject targetEnemy; // �ӽ� ����� ������Ʈ

        for (int z = 0; z < count; z++) // �ʿ�� �ϴ� ������Ʈ �� ��ŭ �ݺ�
        {
            for (int i = 0 + z; i < enemyList.Count; i++) // ���� ������Ʈ Ǯ���� ����Ǿ� �ִ� �� ������Ʈ �� ��ŭ �ݺ�
            {
                if(enemyList[i].activeSelf == false) // ������Ʈ Ǯ���� i��° �� ������Ʈ�� ��Ȱ��ȭ ������ ��
                {
                    if (enemyList[i].name == name) // �ش� ������Ʈ�� ���ϴ� ������Ʈ�� �̸��� ���� ��
                    {
                        targetEnemy = enemyList[i]; // target�� �ش� ������Ʈ �ӽ� ����
                        int j = 0; // ������ for���� �ƴ� ���⼭ �ϴ� ������ �Ʒ��� if������ ����ϱ� ����

                        for (j = 0; j < z; j++) // ����������� �� ������Ʈ ����ŭ �ݺ�
                        {
                            if (i == enemyList.Count - 1) // �̹��� ����Ʈ �������̶��, �ش� �κ����� ���� 1���� �߰� ��� ������, �����ʿ�
                            {
                                SetEnemy(targetEnemy, 1, targetEnemy.name); // ���� �����
                            }

                            if (targetEnemy == target[j]) // �ش� ������Ʈ�� �̹� ������ ��Ͽ� �ִٸ� 
                            {
                                break; // �ݺ��� ����
                            }
                        }

                        if (j == z) // �ݺ����� ������ ������ ��, �� �ش� ������Ʈ�� ������ ��Ͽ� ���ٸ�
                        {
                            target[z] = targetEnemy; // �ش� ������Ʈ�� ������ ��Ͽ� �ִ´�.
                            break; // �ݺ��� ����
                        }
                    }
                }
            }
        }

        return target;
    }

    // �޸� �Ҵ� ����
    public void MemoryClear()
    {
        if (bulletList == null) return; // ����Ʈ�� ��������� ��ȯ

        int listCount = bulletList.Count;

        for (int i = 0; i < listCount; i++)
        {
            GameObject.Destroy(bulletList[i]); // ����Ʈ ���� ������Ʈ ����
        }
        bulletList = null; // ����Ʈ ����

        if (enemyList == null) return; // ����Ʈ�� ��������� ��ȯ

        listCount = enemyList.Count;

        for (int i = 0; i < listCount; i++)
        {
            GameObject.Destroy(enemyList[i]); // ����Ʈ ���� ������Ʈ ����
        }
        enemyList = null; // ����Ʈ ����
    }
}