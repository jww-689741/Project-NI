using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// źȯ ������Ʈ �⺻ ����
[Serializable]
public class BulletInfo
{
    public string name; // �̸�
    public GameObject prefab; // ������Ʈ
    public int count; // ���� ī��Ʈ
}

// �� ������Ʈ �⺻ ����
[Serializable]
public class EnemyInfo
{
    public string name; // �̸�
    public GameObject prefab; // ������Ʈ
    public int count; // ���� ī��Ʈ
}


public class ObjectManager : MonoBehaviour
{
    // �̱���
    public static ObjectManager instance;

    // źȯ ���� �� �θ� ������Ʈ ����
    [Header("Bullet Parent Object")]
    [SerializeField]
    private Transform BulletPool;

    // �� ���� �� �θ� ������Ʈ ����
    [Header("Enemy Parent Object")]
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

    // �� ȣ��
    public GameObject[] GetEnemy(string name, int count)
    {
        int stack = 0;
        GameObject[] target = new GameObject[count];
        GameObject targetEnemy;

        for (int z = stack; z < count; z++)
        {
            for (int i = 0 + z; i < enemyList.Count; i++)
            {
                if (enemyList[i].activeSelf == false)
                {
                    if (enemyList[i].name == name)
                    {
                        targetEnemy = enemyList[i];
                        int j = 0;

                        for (j = 0; j < z; j++)
                        {
                            if (i == enemyList.Count - 1)
                            {
                                SetEnemy(targetEnemy, 1, targetEnemy.name);
                            }

                            if (targetEnemy == target[j])
                            {
                                break;
                            }
                        }

                        if (j == z)
                        {
                            target[z] = targetEnemy;
                            break;
                        }
                    }
                }

                if (i == enemyList.Count - 1)
                {
                    targetEnemy = enemyList[i];
                    SetEnemy(targetEnemy, 1, targetEnemy.name);
                    continue;
                }
            }
        }

        return target;
    }

    // �޸� �Ҵ� ����
    public void MemoryClear()
    {
        int listCount;
        if (bulletList == null && enemyList == null) return; // ��� ����Ʈ�� ��������� ��ȯ
        else if (bulletList != null) // źȯ ����Ʈ�� ������ �ִٸ�
        {
            listCount = bulletList.Count;


            for (int i = 0; i < listCount; i++)
            {
                GameObject.Destroy(bulletList[i]); // ����Ʈ ���� ������Ʈ ����
            }
            bulletList = null; // ����Ʈ ����
        }
        else if (enemyList != null) // �� ����Ʈ�� ������ �ִٸ�
        {
            listCount = enemyList.Count;

            for (int i = 0; i < listCount; i++)
            {
                GameObject.Destroy(enemyList[i]); // ����Ʈ ���� ������Ʈ ����
            }
            enemyList = null; // ����Ʈ ����
        }
    }

}