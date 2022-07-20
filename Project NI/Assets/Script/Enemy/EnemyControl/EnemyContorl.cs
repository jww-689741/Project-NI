using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContorl : MonoBehaviour
{
    public float generationGap;
    public Transform player; // 플레이어 좌표를 저장

    private GameObject[] enemys; // 적 오브젝트 저장 및 반환을 위한 배열
    private Vector3 position = Vector3.zero; // 스폰 지점 값
    private List<int> numberOfEnemy = new List<int>(); // 적 생성 개체 수 리스트, List를 사용한 이유는 없으니 다른 리스트로 변경해도 무방
    private List<Dictionary<string, object>> data;

    void Awake()
    {
        data = ReadCSV.Read("StageInformation");
    }

    void Start()
    {
<<<<<<< HEAD
        StartCoroutine(startSpawn()); // 코루틴 시작
=======
        //StartCoroutine(startSpawn()); // 코루틴 시작
>>>>>>> origin/Pks
    }

    void Update()
    {

    }

    private IEnumerator startSpawn()
    {
        int i = 0; // counts 용

        int sara, billy, betty, irving, selma; // 각 개체수 저장

        while (true) // stop이 true일 동안 반복
        {
            if (data.Count - 1 == i) // data에 더이상 값이 없을 경우
            {
                break; //반복문이 종료된다.
            }

            sara = int.Parse((data[i]["sara"] + "")); // 오브젝트 형 문자열로 변경
            billy = int.Parse((data[i]["billy"] + "")); // 오브젝트 형 문자열로 변경
            betty = int.Parse((data[i]["betty"] + "")); // 오브젝트 형 문자열로 변경
            irving = int.Parse((data[i]["irving"] + "")); // 오브젝트 형 문자열로 변경
            selma = int.Parse((data[i]["selma"] + "")); // 오브젝트 형 문자열로 변경

            if (sara != 0) // 적 1 데이터가 0이 아닐 때
            {
                SpawnEnemy("sara", sara);
            }
            if (billy != 0) // 적 2 데이터가 0이 아닐 때
            {
                SpawnEnemy("billy", billy);
            }
            if (betty != 0) // 적 3 데이터가 0이 아닐 때
            {
                SpawnEnemy("betty", betty);
            }
            if (irving != 0) // 적 4 데이터가 0이 아닐 때
            {
                SpawnEnemy("irving", irving);
            }
            if (selma != 0) // 적 5 데이터가 0이 아닐 때
            {
                SpawnEnemy("selma", selma);
            }
            i++;

            yield return new WaitForSeconds(10f); // 10초 대기
        }
    }

    // 적 오브젝트 스폰 메소드
    private void SpawnEnemy(string name, int count)
    {
<<<<<<< HEAD
        enemys = ObjectManager.instance.GetEnemy(name, count); // 적 count 만큼 리스트로 가져오기
=======
        this.enemys = ObjectManager.instance.GetEnemy(name, count); // 적 count 만큼 리스트로 가져오기
>>>>>>> origin/Pks

        for (int i = 0; i < count; i++) // 리스트의 오브젝트들
        {
            if (enemys[i] != null)
            {
                enemys[i].SetActive(true); // 활성화
                ChangeSpawnPosition(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(100, 160));
                enemys[i].transform.position = position; // 위치 지정
            }        
            else
            {
                Debug.Log("오브젝트 메니저에 " + name + "(이)가 없음");
            }
        }
    }

    // 일렬 횡대
    private void SetHorizontalEnemyPosition(GameObject[] enemys)
    {
        int middle = MiddleNumber(enemys.Length);

        for (int i = 0; i < enemys.Length; i++)
        {
            generationGap = (middle - i) * -2;
            ChangeSpawnPosition(generationGap, 0, 20);
            enemys[i].transform.position = position; // 위치 지정
        }
    }

    // 일렬 종대 
    private void SetVerticalEnemyPosition(GameObject[] enemys)
    {
        int middle = MiddleNumber(enemys.Length);

        for (int i = 0; i < enemys.Length; i++)
        {
            generationGap = (middle - i) * -2;
            ChangeSpawnPosition(0, 0, 20 + generationGap);
            enemys[i].transform.position = position; // 위치 지정
        }
    }

    // 대충 V자
    private void SetVEnemyPosition(GameObject[] enemys)
    {
        int middle = MiddleNumber(enemys.Length);

        for (int i = 0; i < enemys.Length; i++)
        {
            generationGap = (middle - i) * -2;
            ChangeSpawnPosition(21 + generationGap, 16, 150 - Mathf.Abs(generationGap));
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
        position = new Vector3(x + player.position.x, y + player.position.y, z + player.position.z);
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
}