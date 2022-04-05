using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 각 개체당 개별 이동 클레스
public class EnemyMove : MonoBehaviour
{
    public float movementSpeed = 20; // 이동속도
    public Transform player; // 플레이어 좌표를 가져오기 위한 오브젝트

    public float height = 1.0f;

    private bool patternFlag = false; // 패턴 최초 실행 확인용
    private Vector3 target = Vector3.zero; // 목표 지점 임시 저장용 벡터 값, 현재 위치와 비교할 때 사용
    private Vector3 position = Vector3.zero; // 목표 지점 벡터 값, 이 값으로 오브젝트가 움직인다.
    private Rigidbody rb; // 리지드 바디
    private Transform enemyTransform; // 자기 자신의 좌표를 저장
    private delegate void Control(); // 이동용 델리게이트
    Control control; // 선언

    System.Diagnostics.Stopwatch watch; // 곡선 이동 테스트 용

    private void Awake()
    {
        enemyTransform = this.gameObject.transform;
    }
    void Start()
    {
        watch = new System.Diagnostics.Stopwatch(); // 곡선 이동 테스트 용
        watch.Start(); // 곡선 이동 테스트 용
        startTime = Time.time;
        position = enemyTransform.position; // 초기 좌표를 목표 좌표에 입력, 대형을 유지하기 위해 필요
        rb = GetComponent<Rigidbody>(); // 리지드 바디

        // 델리게이트 합연산
        //control += move; // 이동 메소드
        control += SquareMovement; // 이동 메소드
        //control += CheckActive; // 활성화 여부 결정 메소드
    }

    float t;
    float startTime;
    void Update()
    {
        control(); // 델리게이트 control 실행
        t = ((Time.time - startTime) / (watch.ElapsedMilliseconds)) * 550;

        run(t); // 곡선 이동 테스트
    }

    // 이동 메소드
    private void move()
    {
        enemyTransform.LookAt(player); // 플레이어를 바라보게 한다.
        enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, position, movementSpeed * Time.deltaTime); // 현재 좌표에서 목표 좌표로 기제된 속도로 이동
    }

    // 활성화 결정 메소드
    // ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ수정 필요ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    private void CheckActive()
    {
        if (enemyTransform.position.z < player.position.z - 10) // 플레이어 뒤로 10만큼 갔을 때 비활성화
        {
            this.gameObject.SetActive(false); // 비활성화
        }
        else if (enemyTransform.position.x < player.position.x - 50) // 플레이어 보다 50 만큼 왼쪽으로 갔을 때 비활성화
        {
            this.gameObject.SetActive(false); // 비활성화
        }
    }

    // 들이받기
    public void MoveToPlayer()
    {
        position = player.position; // 목표 좌표를 플레이어 좌표로 설정
    }

    // 목표 좌표 변경 메소드
    public void ChangeTarget(float x, float y, float z)
    {
        position.x = x;
        position.y = y;
        position.z = z;
    }

    // 플레이어 기준 목표 좌표 변경 메소드
    public void ChangeTargetToPlayer(float x, float y, float z)
    {
        position.x = x + player.position.x;
        position.y = y + player.position.y;
        position.z = z + player.position.z;
    }

    // 플레이어 기준 회전 메소드
    // 다른 이동 메소드와 같이 실행 되면 이상해지니 주의
    public void RotateAroundPlayer()
    {
        enemyTransform.RotateAround(player.position, Vector3.down, movementSpeed * Time.deltaTime); // 플레이어를 기준으로 회전한다.
    }

    // 곡선 이동, 변경 예정, 테스트 중
    void run(float t)
    {
        Vector3 mep = enemyTransform.position; // 해당 오브젝트 현재 좌표 가져오기

        Vector3 startHeightPos = mep + new Vector3(0, 1, 0) * height;
        Vector3 endHeightPos = position + new Vector3(0, 1, 0) * height;

        Vector3 a = Vector3.Lerp(mep, startHeightPos, t); //Mathf.SmoothStep(tMin, tMax, t)값이 자연스럽게 증가하는건데 생각보다 별로다.
        Vector3 b = Vector3.Lerp(startHeightPos, endHeightPos, t);
        Vector3 c = Vector3.Lerp(endHeightPos, position, t);

        Vector3 d = Vector3.Lerp(a, b, t);
        Vector3 e = Vector3.Lerp(b, c, t);

        Vector3 f = Vector3.Lerp(d, e, t);

        transform.position = f;
    }

    // 자동 좌표 변경 메소드
    // 기괴한 움직임
    void AutoMove()
    {
        Vector3 mep = enemyTransform.position; // 해당 오브젝트 현재 좌표 가져오기
        float x = 0;
        float y = 0;
        float z = 0;

        if (position == enemyTransform.position) // 목표 좌표와 현재 좌표가 같을 때 실행
        {
            x = Random.Range(-20, 20);
            y = Random.Range(-15, 15);
            z = player.transform.position.z - Random.Range(-10, 0);

            ChangeTarget(x, y, z);
        }

        if (mep.z <= player.position.z + 10) // 플레이어와 10만큼 가까워 지면 실행
        {
            z = Random.Range(30, 70);
            ChangeTarget(position.x, position.y, z);
        }
    }

    // 선형이동
    void LinearMovement(bool directionFlag)
    {
        if (directionFlag)
        {
            ChangeTarget(-50 + player.position.x, -5 + position.y, 5 + position.z); // 플레이어 시점 앞에서 왼쪽으로 이동하기, 플레이어 시야 밖으로
        }
        else
        {
            ChangeTarget(50 + player.position.x, -5 + position.y, 5 + position.z); // 플레이어 시점 앞에서 오른쪽으로 이동하기, 플레이어 시야 밖으로
        }
    }

    // 지그제그
    void StepMovement()
    {
        Vector3 mep = enemyTransform.position; // 해당 오브젝트 현재 좌표 가져오기

        bool change = false; // 목표 좌표값 변경 확인용 

        float x = 0;
        float y = 0;
        float z = 0;

        if (!patternFlag) // 해당 패턴을 처음 실행할 때
        {
            patternFlag = true;
            // 시작 지점으로 이동
            x = 30 + player.position.x;
            y = Random.Range(-10, 10);
            z = 80;

            target = new Vector3(x, y, z); // 다음 이동

            ChangeTarget(x, y, z);

        }

        if (position == mep) // 목표 좌표와 현재 좌표가 같을 때 실행
        {
            if (mep.x == target.x && mep.z == 80) // 시작 지점일 때
            {
                // 다음 지점 설정
                x = -30;
                y = Random.Range(-20, 20);
                z = 60;

                change = true; // 이동을 위한 true
                target.x = x + player.position.x;
            }
            else if (mep.x == target.x && mep.z == 60) // 1 지점일 때
            {
                // 다음 지점 설정
                x = 30;
                y = Random.Range(-20, 20);
                z = 40;

                change = true; // 이동을 위한 true
                target.x = x + player.position.x;
            }
            else if (mep.x == target.x && mep.z == 40) // 2 지점일 때
            {
                // 다음 지점 설정
                x = -30;
                y = Random.Range(-20, 20);
                z = 20;

                change = true; // 이동을 위한 true
                target.x = x + player.position.x;
            }
            else if (mep.x == target.x && mep.z == 20) // 3 지점일 때
            {
                // 다음 지점 설정
                x = 30;
                y = Random.Range(-20, 20);
                z = 0;

                change = true; // 이동을 위한 true
                target.x = x + player.position.x;
            }

            if (change) // 위 if 문에서 값이 변경 될 때
            {
                ChangeTargetToPlayer(x, y, z - player.position.z); // 지정 좌표로 이동, 플레이어의 z축 이동이 보류됨에 따른 - 연산
            }
        }
    }

    // 플레이어에게 이동하다가 플레이어와 20(z)만큼 가까워지면 플레이어 옆으로 지나간다. 얼마나 빗겨갈지는 현재 랜덤
    void EvasionPlayer()
    {
        ChangeTarget(player.position.x, Random.Range(-5, 5), player.position.z);

        if (enemyTransform.position.z <= player.position.z + 20)
        {
            ChangeTargetToPlayer(Random.Range(-10, 10), Random.Range(-10, 10), -10);
        }
    }

    // 플레이어 앞에서 선회, 사각형을 그린다.
    void SquareMovement()
    {
        Vector3 mep = enemyTransform.position; // 해당 오브젝트 현재 좌표 가져오기

        float x = 0;
        float y = 0;
        float z = 0;

        bool change = false; // 목표 좌표값 변경 확인용 

        if (position == enemyTransform.position) // 목표 좌표와 현재 좌표가 같을 때 실행
        {
            // 오브젝트가 플레이어 기준 왼쪽에 있고, 플레이어 기준 z 값이 45 보다 높을 때
            if (mep.x < player.position.x && mep.z > player.position.z + 45)
            {
                // 다음 지점 설정
                x = 30;
                y = Random.Range(-10, 10);
                z = 80;

                change = true; // 이동을 위한 true
            }
            // 오브젝트가 플레이어 기준 오른쪽에 있고, 플레이어 기준 z 값이 45 보다 높을 때
            else if (mep.x > player.position.x && mep.z > player.position.z + 45)
            {
                // 다음 지점 설정
                x = 30;
                y = Random.Range(-10, 10);
                z = 40;

                change = true; // 이동을 위한 true
            }
            // 오브젝트가 플레이어 기준 오른쪽에 있고, 플레이어 기준 z 값이 45 보다 낮을 때
            else if (mep.x > player.position.x && mep.z < player.position.z + 45)
            {
                // 다음 지점 설정
                x = -30;
                y = Random.Range(-10, 10);
                z = 40;

                change = true; // 이동을 위한 true
            }
            // 오브젝트가 플레이어 기준 왼쪽에 있고, 플레이어 기준 z 값이 45 보다 낮을 때
            else if (mep.x < player.position.x && mep.z < player.position.z + 45)
            {
                // 다음 지점 설정
                x = -30;
                y = Random.Range(-10, 10);
                z = 80;

                change = true; // 이동을 위한 true
            }

            if (change) // 위 if 문에서 값이 변경 될 때, 즉 목표 좌표가 변경될 때
            {
                ChangeTargetToPlayer(x, y, z - player.position.z); // 지정 좌표로 이동, 플레이어의 z축 이동이 보류됨에 따른 - 연산
            }
        }
    }
}