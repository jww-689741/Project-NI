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

<<<<<<< HEAD
=======
[Serializable]
public class EnemyInfo // ������ų �� ������Ʈ�� ������ ������ Ŭ����
{
    public EnemyInfo(String name, GameObject prefab, int count)
    {
        this.name = name;
        this.prefab = prefab;
        this.count = count;
    }

    public string name; // ������Ʈ �̸�
    public GameObject prefab; // ������Ʈ ������
    public int count; // ������Ʈ ���� ī��Ʈ
}
>>>>>>> origin/My

public class ObjectManager : MonoBehaviour
{
    // �̱���
    public static ObjectManager instance;

    // ���� �� �θ� ������Ʈ ����
    [Header("Parent Object")]
    [SerializeField]
<<<<<<< HEAD
    private Transform BulletPool;
=======
    Transform parent; // ������ Ŭ�� ������Ʈ�� �����ϴ� �����

    public List<Queue<GameObject>> enemyList; // ������Ʈ�� ����Ʈ
    public List<Queue<GameObject>> bulletList; // ������Ʈ�� ����Ʈ
>>>>>>> origin/My

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

    // ������Ʈ ���� ����
    private void Start()
    {
<<<<<<< HEAD
        if(bulletinfo.Length < 1) SetBullet(bulletinfo[0].prefab, bulletinfo[0].count, bulletinfo[0].name); // ����Ʈ�� ������ �ϳ��� ���
        else // ����Ʈ�� ������ �� �̻��� ���
        {
            for (int i = 0; i < bulletinfo.Length; i++)
            {
                SetBullet(bulletinfo[i].prefab, bulletinfo[i].count, bulletinfo[i].name);
            }
        }
    }
    
    // źȯ ����, �Ӽ� �Է�, ����Ʈ ����
    public void SetBullet(GameObject bullet,int count,string name)
    {
        for(int i = 0; i < count; i++)
        {
            GameObject clone = Instantiate(bullet) as GameObject; // Ŭ�� ����
            clone.transform.name = name; // �̸�
            clone.transform.localPosition = Vector3.zero; // ��ġ
            clone.SetActive(false); // ��Ȱ��ȭ
            clone.transform.parent = BulletPool; // �θ� ������Ʈ ����
            bulletList.Add(clone); // ����Ʈ ����
        }
=======
        enemyList = new List<Queue<GameObject>>();
        bulletList = new List<Queue<GameObject>>();
        InsertObject();
    }
    private void Update()
    {
        MouseClickListener();
    }

    public void MouseClickListener()
    {
        if (Input.GetMouseButtonUp(0))
        {
            InsertObject(1);
        }
        transform.Translate(Vector3.forward);
>>>>>>> origin/My
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

            if(targetBullet.activeSelf == true) // źȯ�� Ȱ��ȭ ��
            {
                if(i == listCount - 1) // ����Ʈ �� ��� źȯ�� Ȱ��ȭ ������ ��
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

<<<<<<< HEAD
    // �޸� �Ҵ� ����
    public void MemoryClear()
=======
    private void InsertObject(int n) // ������ ��ϵ� ������Ʈ ť�� ����Ʈ�� �����ϴ� �޼ҵ�
    {
        if (bulletInfo != null)
        {
            for (int i = 0; i < bulletInfo.Length; i++)
            {
                bulletList.Add(EnqueueObject(bulletInfo[i])); // ����Ʈ�� ��ȯ�� ������Ʈ ť�� ����
            }
        }
    }

    Queue<GameObject> EnqueueObject(EnemyInfo magazineInfoPrefab) // ����Ʈ ���� ť�� ������ ��ϵ� ������Ʈ�� Ŭ���� �����ϰ� �����ϴ� �޼ҵ�
>>>>>>> origin/My
    {
        if (bulletList == null) return; // ����Ʈ�� ��������� ��ȯ

        int listCount = bulletList.Count;

        for(int i = 0; i < listCount; i++)
        {
<<<<<<< HEAD
            GameObject.Destroy(bulletList[i]); // ����Ʈ ���� ������Ʈ ����
=======
            GameObject clone = Instantiate(magazineInfoPrefab.prefab); // ����� ������Ʈ�� Ŭ�� ����
            //clone.SetActive(false); // Ŭ�� ������Ʈ ��Ȱ��ȭ
            clone.transform.SetParent(parent); // ������ Ŭ���� ������Ʈ ����� ������Ʈ�� �ڽ� ������Ʈ�� ��ġ
            returnQueue.Enqueue(clone); // Ŭ�� ������Ʈ�� ť�� ����
        }

        return returnQueue; // ��ȯ
    }
    Queue<GameObject> EnqueueObject(BulletInfo magazineInfoPrefab) // ����Ʈ ���� ť�� ������ ��ϵ� ������Ʈ�� Ŭ���� �����ϰ� �����ϴ� �޼ҵ�
    {
        Queue<GameObject> returnQueue = new Queue<GameObject>(); // ��ȯ �� ť

        for (int i = 0; i < magazineInfoPrefab.count; i++) // ����� ī��Ʈ��ŭ ����
        {
            GameObject clone = Instantiate(magazineInfoPrefab.prefab); // ����� ������Ʈ�� Ŭ�� ����
            //clone.SetActive(false); // Ŭ�� ������Ʈ ��Ȱ��ȭ
            clone.transform.SetParent(parent); // ������ Ŭ���� ������Ʈ ����� ������Ʈ�� �ڽ� ������Ʈ�� ��ġ
            returnQueue.Enqueue(clone); // Ŭ�� ������Ʈ�� ť�� ����
>>>>>>> origin/My
        }
        bulletList = null; // ����Ʈ ����
    }

}
