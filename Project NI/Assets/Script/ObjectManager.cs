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


public class ObjectManager : MonoBehaviour
{
    // �̱���
    public static ObjectManager instance;

    // ���� �� �θ� ������Ʈ ����
    [Header("Parent Object")]
    [SerializeField]
    private Transform BulletPool;

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

    // �޸� �Ҵ� ����
    public void MemoryClear()
    {
        if (bulletList == null) return; // ����Ʈ�� ��������� ��ȯ

        int listCount = bulletList.Count;

        for(int i = 0; i < listCount; i++)
        {
            GameObject.Destroy(bulletList[i]); // ����Ʈ ���� ������Ʈ ����
        }
        bulletList = null; // ����Ʈ ����
    }

}
