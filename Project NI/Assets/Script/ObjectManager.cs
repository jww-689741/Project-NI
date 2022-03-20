using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BulletInfo // ������ų źȯ ������Ʈ�� ������ ������ Ŭ����
{
    public string name; // ������Ʈ �̸�
    public GameObject prefab; // ������Ʈ ������
    public int count; // ������Ʈ ���� ī��Ʈ
}

[Serializable]
public class EnemyInfo // ������ų �� ������Ʈ�� ������ ������ Ŭ����
{
    public string name; // ������Ʈ �̸�
    public GameObject prefab; // ������Ʈ ������
    public int count; // ������Ʈ ���� ī��Ʈ
}

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance; // �ٸ� � ��ũ��Ʈ������ ��밡���ϵ��� �̱��� ���� ����

    [SerializeField]
    BulletInfo[] bulletInfo = null; // ��� �� źȯ ������Ʈ�� ������ ����Ʈ

    [SerializeField]
    EnemyInfo[] enemyInfo = null; // ��� �� �� ������Ʈ�� ������ ����Ʈ

    [Header("Object Storage")]
    [SerializeField]
    Transform parent; // ������ Ŭ�� ������Ʈ�� �����ϴ� �����

    public List<Queue<GameObject>> enemyList; // ������Ʈ�� ����Ʈ

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        enemyList = new List<Queue<GameObject>>();
        InsertObject();
    }

    private void InsertObject() // ������ ��ϵ� ������Ʈ ť�� ����Ʈ�� �����ϴ� �޼ҵ�
    {
        if(enemyInfo != null)
        {
            for(int i = 0;i< enemyInfo.Length; i++)
            {
                enemyList.Add(EnqueueObject(enemyInfo[i])); // ����Ʈ�� ��ȯ�� ������Ʈ ť�� ����
            }
        }
    }

    Queue<GameObject> EnqueueObject(EnemyInfo magazineInfoPrefab) // ����Ʈ ���� ť�� ������ ��ϵ� ������Ʈ�� Ŭ���� �����ϰ� �����ϴ� �޼ҵ�
    {
        Queue<GameObject> returnQueue = new Queue<GameObject>(); // ��ȯ �� ť

        for(int i = 0; i < magazineInfoPrefab.count; i++) // ����� ī��Ʈ��ŭ ����
        {
            GameObject clone = Instantiate(magazineInfoPrefab.prefab) as GameObject; // ����� ������Ʈ�� Ŭ�� ����
            clone.SetActive(false); // Ŭ�� ������Ʈ ��Ȱ��ȭ
            clone.transform.SetParent(parent); // ������ Ŭ���� ������Ʈ ����� ������Ʈ�� �ڽ� ������Ʈ�� ��ġ
            returnQueue.Enqueue(clone); // Ŭ�� ������Ʈ�� ť�� ����
        }

        return returnQueue; // ��ȯ
    }
}
