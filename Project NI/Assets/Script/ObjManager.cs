using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjManager : MonoBehaviour
{
    //싱글톤
    static ObjManager st;
    public static ObjManager Call() { return st; }
    private void Awake()                                           //게임종료 후 메모리 날려버림
    {
        st = this;
    }
    private void OnDestroy()
    {
        MemoryDelete();
        st = null;
    }
    public GameObject[] Origin;                                     // 프리팹 원본
    public List<GameObject> Manager;                                // 생성된 객체들을 저장할 리스트
    void Start()
    {
        SetObject(Origin[0], 20, "Bullet");                         // 총알을 생성
    }

    public void SetObject(GameObject _Obj,int _Count, string _Name)
    {
        for(int i = 0; i<_Count;i++)
        {
            GameObject obj = Instantiate(_Obj) as GameObject;
            obj.transform.name = _Name;                             // 이름을 정한다
            obj.transform.localPosition = Vector3.zero;             // 위치를 정한다
            obj.SetActive(false);                                   // 객체를 비활성화
            obj.transform.parent = transform;                       // 매니저 객체의 자식으로
            Manager.Add(obj);                                       // 리스트에 저장
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
