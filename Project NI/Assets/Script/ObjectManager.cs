using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BulletInfo // 생성시킬 탄환 오브젝트의 정보를 정의할 클래스
{
    public string name; // 오브젝트 이름
    public GameObject prefab; // 오브젝트 프리팹
    public int count; // 오브젝트 생성 카운트
}

[Serializable]
public class EnemyInfo // 생성시킬 적 오브젝트의 정보를 정의할 클래스
{
    public EnemyInfo(String name, GameObject prefab, int count)
    {
        this.name = name;
        this.prefab = prefab;
        this.count = count;
    }

    public string name; // 오브젝트 이름
    public GameObject prefab; // 오브젝트 프리팹
    public int count; // 오브젝트 생성 카운트
}

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance; // 다른 어떤 스크립트에서도 사용가능하도록 싱글톤 패턴 적용

    [SerializeField]
    BulletInfo[] bulletInfo = null; // 등록 할 탄환 오브젝트를 저장할 리스트

    [SerializeField]
    EnemyInfo[] enemyInfo = null; // 등록 할 적 오브젝트를 저장할 리스트

    [Header("Object Storage")]
    [SerializeField]
    Transform parent; // 생성된 클론 오브젝트를 저장하는 저장소

    public List<Queue<GameObject>> enemyList; // 오브젝트의 리스트
    public List<Queue<GameObject>> bulletList; // 오브젝트의 리스트

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }

    private void Start()
    {
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
    }

    private void InsertObject() // 정보가 등록된 오브젝트 큐를 리스트에 저장하는 메소드
    {
        if(enemyInfo != null)
        {
            for(int i = 0;i< enemyInfo.Length; i++)
            {
                enemyList.Add(EnqueueObject(enemyInfo[i])); // 리스트에 반환된 오브젝트 큐를 삽입
            }
        }
    }

    private void InsertObject(int n) // 정보가 등록된 오브젝트 큐를 리스트에 저장하는 메소드
    {
        if (bulletInfo != null)
        {
            for (int i = 0; i < bulletInfo.Length; i++)
            {
                bulletList.Add(EnqueueObject(bulletInfo[i])); // 리스트에 반환된 오브젝트 큐를 삽입
            }
        }
    }

    Queue<GameObject> EnqueueObject(EnemyInfo magazineInfoPrefab) // 리스트 내부 큐에 정보가 등록된 오브젝트의 클론을 생성하고 저장하는 메소드
    {
        Queue<GameObject> returnQueue = new Queue<GameObject>(); // 반환 할 큐

        for(int i = 0; i < magazineInfoPrefab.count; i++) // 등록한 카운트만큼 루프
        {
            GameObject clone = Instantiate(magazineInfoPrefab.prefab); // 등록한 오브젝트의 클론 생성
            //clone.SetActive(false); // 클론 오브젝트 비활성화
            clone.transform.SetParent(parent); // 생성한 클론을 오브젝트 저장소 오브젝트의 자식 오브젝트로 위치
            returnQueue.Enqueue(clone); // 클론 오브젝트를 큐에 삽입
        }

        return returnQueue; // 반환
    }
    Queue<GameObject> EnqueueObject(BulletInfo magazineInfoPrefab) // 리스트 내부 큐에 정보가 등록된 오브젝트의 클론을 생성하고 저장하는 메소드
    {
        Queue<GameObject> returnQueue = new Queue<GameObject>(); // 반환 할 큐

        for (int i = 0; i < magazineInfoPrefab.count; i++) // 등록한 카운트만큼 루프
        {
            GameObject clone = Instantiate(magazineInfoPrefab.prefab); // 등록한 오브젝트의 클론 생성
            //clone.SetActive(false); // 클론 오브젝트 비활성화
            clone.transform.SetParent(parent); // 생성한 클론을 오브젝트 저장소 오브젝트의 자식 오브젝트로 위치
            returnQueue.Enqueue(clone); // 클론 오브젝트를 큐에 삽입
        }

        return returnQueue; // 반환
    }
}
