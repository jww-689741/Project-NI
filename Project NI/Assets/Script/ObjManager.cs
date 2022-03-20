using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjManager : MonoBehaviour
{
    //�̱���
    static ObjManager st;
    public static ObjManager Call() { return st; }
    private void Awake()                                           //�������� �� �޸� ��������
    {
        st = this;
    }
    private void OnDestroy()
    {
        MemoryDelete();
        st = null;
    }
    public GameObject[] Origin;                                     // ������ ����
    public List<GameObject> Manager;                                // ������ ��ü���� ������ ����Ʈ
    void Start()
    {
        SetObject(Origin[0], 20, "Bullet");                         // �Ѿ��� ����
    }

    public void SetObject(GameObject _Obj,int _Count, string _Name)
    {
        for(int i = 0; i<_Count;i++)
        {
            GameObject obj = Instantiate(_Obj) as GameObject;
            obj.transform.name = _Name;                             // �̸��� ���Ѵ�
            obj.transform.localPosition = Vector3.zero;             // ��ġ�� ���Ѵ�
            obj.SetActive(false);                                   // ��ü�� ��Ȱ��ȭ
            obj.transform.parent = transform;                       // �Ŵ��� ��ü�� �ڽ�����
            Manager.Add(obj);                                       // ����Ʈ�� ����
        }
    }

    public GameObject GetObject(string _Name)
    {
        if (Manager == null)
            return null;

        int Count = Manager.Count;
        for(int i = 0; i< Count; i++)
        {
            if (_Name != Manager[i].name)
                continue;

            GameObject Obj = Manager[i];

            if(Obj.active == true)
            {
                if(i == Count -1)
                {
                    SetObject(Obj, 1, "Bullet");
                    return Manager[i + 1];
                }
                continue;
            }
            return Manager[i];
        }
        return null;
    }

    public void MemoryDelete()
    {
        if (Manager == null)
            return;

        int Count = Manager.Count;

        for(int i =0; i<Count; i++)
        {
            GameObject obj = Manager[i];
            GameObject.Destroy(obj);
        }
        Manager = null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
