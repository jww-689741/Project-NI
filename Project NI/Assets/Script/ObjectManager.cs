using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 탄환 오브젝트 기본 정보
[Serializable]
public class BulletInfo
{
    public string name; // 이름
    public GameObject prefab; // 오브젝트
    public int count; // 생성 카운트
}

<<<<<<< HEAD
=======
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
>>>>>>> origin/My

public class ObjectManager : MonoBehaviour
{
    // 싱글톤
    public static ObjectManager instance;

    // 생성 시 부모 오브젝트 설정
    [Header("Parent Object")]
    [SerializeField]
<<<<<<< HEAD
    private Transform BulletPool;
=======
    Transform parent; // 생성된 클론 오브젝트를 저장하는 저장소

    public List<Queue<GameObject>> enemyList; // 오브젝트의 리스트
    public List<Queue<GameObject>> bulletList; // 오브젝트의 리스트
>>>>>>> origin/My

    // 싱글톤 적용
    private void Awake()
    {
        instance = this;
    }

    // 게임 종료 시 메모리 할당해제
    private void OnDestroy()
    {
        MemoryClear();
    }

    public BulletInfo[] bulletinfo = null; // 탄환 오브젝트 정보의 배열
    public List<GameObject> bulletList; // 생성한 탄환 리스트

    // 오브젝트 사전 생성
    private void Start()
    {
<<<<<<< HEAD
        if(bulletinfo.Length < 1) SetBullet(bulletinfo[0].prefab, bulletinfo[0].count, bulletinfo[0].name); // 리스트의 내용이 하나일 경우
        else // 리스트의 내용이 둘 이상인 경우
        {
            for (int i = 0; i < bulletinfo.Length; i++)
            {
                SetBullet(bulletinfo[i].prefab, bulletinfo[i].count, bulletinfo[i].name);
            }
        }
    }
    
    // 탄환 생성, 속성 입력, 리스트 삽입
    public void SetBullet(GameObject bullet,int count,string name)
    {
        for(int i = 0; i < count; i++)
        {
            GameObject clone = Instantiate(bullet) as GameObject; // 클론 생성
            clone.transform.name = name; // 이름
            clone.transform.localPosition = Vector3.zero; // 위치
            clone.SetActive(false); // 비활성화
            clone.transform.parent = BulletPool; // 부모 오브젝트 설정
            bulletList.Add(clone); // 리스트 삽입
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

    // 탄환 호출
    public GameObject GetBullet(string name)
    {
        if (bulletList == null) return null; // 리스트가 비어있으면 null 반환
        
        int listCount = bulletList.Count;
        for (int i = 0; i < listCount; i++)
        {
            if (name != bulletList[i].name) continue; // 찾는 이름의 탄환이 없으면 넘어감

            GameObject targetBullet = bulletList[i]; // 찾는 탄환의 오브젝트

            if(targetBullet.activeSelf == true) // 탄환이 활성화 시
            {
                if(i == listCount - 1) // 리스트 내 모든 탄환이 활성화 상태일 때
                {
                    SetBullet(targetBullet, 1, targetBullet.name); // 새로운 탄환 1개 추가
                    return bulletList[i + 1]; // 새로 생성된 탄환 반환
                }
                continue;
            }
            return bulletList[i]; // 비활성화 상태인 탄환 반환
        }
        return null; // 호출 할 탄환의 이름을 찾지 못한 경우 null반환
    }

<<<<<<< HEAD
    // 메모리 할당 해제
    public void MemoryClear()
=======
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
>>>>>>> origin/My
    {
        if (bulletList == null) return; // 리스트가 비어있으면 반환

        int listCount = bulletList.Count;

        for(int i = 0; i < listCount; i++)
        {
<<<<<<< HEAD
            GameObject.Destroy(bulletList[i]); // 리스트 내에 오브젝트 삭제
=======
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
>>>>>>> origin/My
        }
        bulletList = null; // 리스트 비우기
    }

}
