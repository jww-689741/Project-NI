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

// 적 오브젝트 기본 정보
[Serializable]
public class EnemyInfo
{
    public string name; // 이름
    public GameObject prefab; // 오브젝트
    public int count; // 생성 카운트
}

// 아이템 오브젝트 기본 정보
[Serializable]
public class ItemInfo
{
    public string name; // 이름
    public GameObject prefab; // 오브젝트
    public int count; // 생성 카운트
}

public class ObjectManager : MonoBehaviour
{
    // 싱글톤
    public static ObjectManager instance;

    // 탄환 생성 시 부모 오브젝트 설정
    [Header("Bullet Parent Object")]
    [SerializeField]
    private Transform bulletPool;

    // 적 생성 시 부모 오브젝트 설정
    [Header("Enemy Parent Object")]
    [SerializeField]
    private Transform enemyPool;

    // 적 생성 시 부모 오브젝트 설정
    [Header("Item Parent Object")]
    [SerializeField]
    private Transform itemPool;

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

    public EnemyInfo[] enemyinfo = null; // 적 오브젝트 정보의 배열
    public List<GameObject> enemyList; // 생성한 적 리스트

    public EnemyInfo[] iteminfo = null; // 적 오브젝트 정보의 배열
    public List<GameObject> itemList; // 생성한 적 리스트


    // 오브젝트 사전 생성
    private void Start()
    {
        if (bulletinfo.Length == 1) SetBullet(bulletinfo[0].prefab, bulletinfo[0].count, bulletinfo[0].name); // 리스트의 내용이 하나일 경우
        else if(bulletinfo.Length > 1) // 리스트의 내용이 둘 이상인 경우
        {
            for (int i = 0; i < bulletinfo.Length; i++)
            {
                SetBullet(bulletinfo[i].prefab, bulletinfo[i].count, bulletinfo[i].name);
            }
        }

        // 적 세팅
        if (enemyinfo.Length == 1) SetEnemy(enemyinfo[0].prefab, enemyinfo[0].count, enemyinfo[0].name); // 리스트의 내용이 하나일 경우
        else if (bulletinfo.Length > 1) // 리스트의 내용이 둘 이상인 경우
        {
            for (int i = 0; i < enemyinfo.Length; i++)
            {
                SetEnemy(enemyinfo[i].prefab, enemyinfo[i].count, enemyinfo[i].name);
            }
        }

    }

    // 탄환 생성, 속성 입력, 리스트 삽입
    public void SetBullet(GameObject bullet, int count, string name)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject clone = Instantiate(bullet) as GameObject; // 클론 생성
            clone.transform.name = name; // 이름
            clone.transform.localPosition = Vector3.zero; // 위치
            clone.SetActive(false); // 비활성화
            clone.transform.parent = bulletPool; // 부모 오브젝트 설정
            bulletList.Add(clone); // 리스트 삽입
        }
    }

    // 적 생성, 속성 입력, 리스트 삽입
    public void SetEnemy(GameObject enemy, int count, string name)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject clone = Instantiate(enemy) as GameObject; // 클론 생성
            clone.transform.name = name; // 이름
            clone.transform.localPosition = Vector3.zero; // 위치
            clone.SetActive(false); // 비활성화
            clone.transform.parent = enemyPool; // 부모 오브젝트 설정
            enemyList.Add(clone); // 리스트 삽입
        }
    }

    // 아이템 생성, 속성 입력, 리스트 삽입
    public void SetItem(GameObject item, int count, string name)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject clone = Instantiate(item) as GameObject; // 클론 생성
            clone.transform.name = name; // 이름
            clone.transform.localPosition = Vector3.zero; // 위치
            clone.SetActive(false); // 비활성화
            clone.transform.parent = enemyPool; // 부모 오브젝트 설정
            enemyList.Add(clone); // 리스트 삽입
        }
    }

    // 탄환 호출
    public GameObject GetBullet(string name)
    {
        if (bulletList == null) return null; // 리스트가 비어있으면 null 반환

        int listCount = bulletList.Count;
        int targetCount = 0;
        GameObject targetBullet;
        for (int i = 0; i < listCount; i++)
        {
            if (name != bulletList[i].name) continue; // 찾는 이름의 탄환이 없으면 넘어감
            else ++targetCount;
        }

        for (int i = 0; i < listCount; i++)
        {
            if (name != bulletList[i].name) continue; // 찾는 이름의 탄환이 없으면 넘어감
            else
            {
                targetBullet = bulletList[i]; // 찾는 탄환의 오브젝트
            }

            if (targetBullet.activeSelf == true) // 탄환이 활성화 시
            {
                if (i == targetCount - 1) // 리스트 내 모든 탄환이 활성화 상태일 때
                {
                    SetBullet(targetBullet, 1, targetBullet.name); // 새로운 탄환 1개 추가
                    return bulletList[listCount + 1]; // 새로 생성된 탄환 반환
                }
                continue;
            }
            return bulletList[i]; // 비활성화 상태인 탄환 반환
        }
        return null; // 호출 할 탄환의 이름을 찾지 못한 경우 null반환
    }

    // 적 다중 호출
    public GameObject[] GetEnemy(string name, int count)
    {
        GameObject[] target = new GameObject[count]; // 가져갈 적 오브젝트 저장용 배열
        GameObject targetEnemy; // 임시 저장용 오브젝트

        for (int z = 0; z < count; z++) // 필요로 하는 오브젝트 수 만큼 반복
        {
            for (int i = 0 + z; i < enemyList.Count; i++) // 현재 오브젝트 풀링에 저장되어 있는 적 오브젝트 수 만큼 반복
            {
                if (enemyList[i].activeSelf == false) // 오브젝트 풀링의 i번째 적 오브젝트가 비활성화 상태일 때
                {
                    if (enemyList[i].name == name) // 해당 오브젝트와 원하는 오브젝트의 이름이 같을 때
                    {
                        targetEnemy = enemyList[i]; // target에 해당 오브젝트 임시 저장
                        int j = 0; // 선언을 for문이 아닌 여기서 하는 이유는 아래의 if문에서 사용하기 위함

                        for (j = 0; j < z; j++) // 가져가기로한 적 오브젝트 수만큼 반복
                        {
                            if (i == enemyList.Count - 1) // 이번이 리스트 마지막이라면, 해당 부분으로 인해 1개의 추가 요소 생성됨, 수정필요
                            {
                                SetEnemy(targetEnemy, 1, targetEnemy.name); // 새로 만들기
                            }

                            if (targetEnemy == target[j]) // 해당 오브젝트가 이미 가져갈 목록에 있다면 
                            {
                                break; // 반복문 종료
                            }
                        }

                        if (j == z) // 반복문이 끝까지 돌았을 때, 즉 해당 오브젝트가 가져갈 목록에 없다면
                        {
                            target[z] = targetEnemy; // 해당 오브젝트를 가져갈 목록에 넣는다.
                            break; // 반복문 종료
                        }
                    }
                }
            }
        }

        return target;
    }

    // 메모리 할당 해제
    public void MemoryClear()
    {
        int listCount;
        if (bulletList == null && enemyList == null) return; // 모든 리스트가 비어있으면 반환
        else if (bulletList != null) // 탄환 리스트가 내용이 있다면
        {
            listCount = bulletList.Count;


            for (int i = 0; i < listCount; i++)
            {
                GameObject.Destroy(bulletList[i]); // 리스트 내에 오브젝트 삭제
            }
            bulletList = null; // 리스트 비우기
        }
        else if (enemyList != null) // 적 리스트가 내용이 있다면
        {
            listCount = enemyList.Count;

            for (int i = 0; i < listCount; i++)
            {
                GameObject.Destroy(enemyList[i]); // 리스트 내에 오브젝트 삭제
            }
            enemyList = null; // 리스트 비우기
        }
    }

}