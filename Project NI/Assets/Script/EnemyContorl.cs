using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContorl : MonoBehaviour
{
    GameObject player; // 플레이어 좌표를 가져오기 위한 오브젝트
    GameObject[] enemys; // 적 오브젝트 저장 및 반환을 위한 배열
    List<int> counts = new List<int> (); // 적 생성 개체 수 리스트, List를 사용한 이유는 없으니 다른 리스트로 변경해도 무방
    int padd;

    Vector3 position = Vector3.zero; // 스폰 지점 값

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // 플레이어 오브젝트 가져오기
        StartCoroutine(testCoroutine()); // 코루틴 시작
    }

    void Update()
    {

    }

    private IEnumerator testCoroutine() 
    {
        int i = 0; // counts 용
        bool stop = true;

        counts.Add(1); // counts에 1 추가

        while (stop) // stop이 true일 동안 반복
        {
            yield return new WaitForSeconds(3f); // 3초 대기

            SpawnEnemy(counts[i]); // counts[i]번째 수 만큼 적 오브젝트 생성

            if (counts.Count - 1 == i) // counts에 더이상 값이 없을 경우
            {
                stop = false; // stop을 false로 변경, 이후 반복문이 종료된다.
            }

            i++;
        }
    }

    // 적 오브젝트 스폰 메소드
    private void SpawnEnemy(int count)
    {
        enemys = TestObjectManager.instance.GetEnemy("test", count); // 적 count 만큼 리스트로 가져오기

        for (int i = 0; i < count; i++) // 리스트의 오브젝트들
        {
            enemys[i].SetActive(true); // 활성화
        }

        SetVEnemyPosition(enemys); // 대형 설정
    }

    // 일렬 횡대
    private void SetHorizontalEnemyPosition(GameObject[] enemys)
    {
        int middle = MiddleNumber(enemys.Length);

        for (int i = 0; i < enemys.Length; i++)
        {
            padd = (middle - i) * -2;
            ChangeSpawnPosition(padd, 0, 20);
            enemys[i].transform.position = position; // 위치 지정
        }
    }

    // 일렬 종대 
    private void SetVerticalEnemyPosition(GameObject[] enemys)
    {
        int middle = MiddleNumber(enemys.Length);

        for (int i = 0; i < enemys.Length; i++)
        {
            padd = (middle - i) * -2;
            ChangeSpawnPosition(0, 0, 20 + padd);
            enemys[i].transform.position = position; // 위치 지정
        }
    }

    // 대충 V자
    private void SetVEnemyPosition(GameObject[] enemys)
    {
        int middle = MiddleNumber(enemys.Length);

        for (int i = 0; i < enemys.Length; i++)
        {
            padd = (middle - i) * -2;
            ChangeSpawnPosition(21 + padd, 16, 150 - abs(padd));
            enemys[i].transform.position = position; // 위치 지정
        }
    }

    // 포지션 설정 중 중앙에 올 친구 번호 구하기
    private int MiddleNumber(int count)
    {
        return Mathf.CeilToInt(count / 2); // 중앙에 있을 오브젝트 번호
    }

    // 적 오브젝트 스폰 지점 벡터 값 변경
    private void ChangeSpawnPosition(float x, float y, float z)
    {
        position = new Vector3(x + player.transform.position.x, y + player.transform.position.y, z + player.transform.position.z);
    }

    // 기존 스폰 지점 벡터 값에 추가로 값을 더하는 메소드
    private void AddSpawnPosition(GameObject[] enemy, float x, float y, float z)
    {
        for (int i = 0; i < enemy.Length; i++)
        {
            position = new Vector3(x + enemys[i].transform.position.x, y + enemys[i].transform.position.y, z + enemys[i].transform.position.z);
            enemys[i].transform.position = position;
        }
    }

    // 절대값, 라이브러리 쓰려다 뭔가 문제가 있었어서 만들어서 씀, 문제는 기억이 안남
    private int abs(int num)
    {
        if (num < 0)
        {
            return num * -1;
        }
        else
            return num;
    }
}
