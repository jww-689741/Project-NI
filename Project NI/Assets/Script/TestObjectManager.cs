using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// 탄환 오브젝트 기본 정보
[Serializable]
public class BulletInfo
{
    public string name; // 이름
    public GameObject prefab; // 오브젝트
    public int count; // 생성 카운트
}

[Serializable]
public class EnemyInfo
{
    public string name; // 이름
    public GameObject prefab; // 오브젝트
    public int count; // 생성 카운트
}

public class TestObjectManager : MonoBehaviour
{
    // 싱글톤
    public static TestObjectManager instance;

    // 생성 시 부모 오브젝트 설정
    [Header("Parent Object")]
    [SerializeField]
    private Transform BulletPool;

    [Header("Parent Object")]
    [SerializeField]
    private Transform EnemyPool;

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

    // 오브젝트 사전 생성
    private void Start()
    {
        // 탄환 세팅
        if (bulletinfo.Length < 1) SetBullet(bulletinfo[0].prefab, bulletinfo[0].count, bulletinfo[0].name); // 리스트의 내용이 하나일 경우
        else // 리스트의 내용이 둘 이상인 경우
        {
            for (int i = 0; i < bulletinfo.Length; i++)
            {
                SetBullet(bulletinfo[i].prefab, bulletinfo[i].count, bulletinfo[i].name);
            }
        }

        // 적 세팅
        if (enemyinfo.Length < 1) SetEnemy(enemyinfo[0].prefab, enemyinfo[0].count, enemyinfo[0].name); // 리스트의 내용이 하나일 경우
        else // 리스트의 내용이 둘 이상인 경우
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
            clone.transform.parent = BulletPool; // 부모 오브젝트 설정
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
            clone.transform.parent = EnemyPool; // 부모 오브젝트 설정
            enemyList.Add(clone); // 리스트 삽입
        }
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

            if (targetBullet.activeSelf == true) // 탄환이 활성화 시
            {
                if (i == listCount - 1) // 리스트 내 모든 탄환이 활성화 상태일 때
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

    public GameObject[] GetEnemy(string name, int count)
    {
        int stack = 0;
        GameObject[] target = new GameObject[count];
        GameObject targetEnemy;

        for (int z = stack; z < count; z++)
        {
            for (int i = 0 + z; i < enemyList.Count; i++)
            {
                if(enemyList[i].activeSelf == false) 
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

    // 메모리 할당 해제
    public void MemoryClear()
    {
        if (bulletList == null) return; // 리스트가 비어있으면 반환

        int listCount = bulletList.Count;

        for (int i = 0; i < listCount; i++)
        {
            GameObject.Destroy(bulletList[i]); // 리스트 내에 오브젝트 삭제
        }
        bulletList = null; // 리스트 비우기

        if (enemyList == null) return; // 리스트가 비어있으면 반환

        listCount = enemyList.Count;

        for (int i = 0; i < listCount; i++)
        {
            GameObject.Destroy(enemyList[i]); // 리스트 내에 오브젝트 삭제
        }
        enemyList = null; // 리스트 비우기
    }
}