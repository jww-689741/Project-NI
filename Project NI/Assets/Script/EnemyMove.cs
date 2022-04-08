using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 각 개체당 개별 이동 클레스
public class EnemyMove : MonoBehaviour
{
    Vector3 target = new Vector3(0, 0, 0); // 목표 지점 임시 저장용 벡터 값, 현재 위치와 비교할 때 사용
    Vector3 position = new Vector3(0, 0, 0); // 목표 지점 벡터 값, 이 값으로 오브젝트가 움직인다.
    public float movementSpeed = 20; // 이동속도
    GameObject player; // 플레이어 좌표를 가져오기 위한 오브젝트
    Rigidbody rb; // 리지드 바디

    int count = 0; // 패턴 최초 실행 확인용
    public float height = 1.0f;

    private delegate void Control(); // 이동용 델리게이트
    Control control; // 선언

    System.Diagnostics.Stopwatch watch; // 곡선 이동 테스트 용
    void Start()
    {
        watch = new System.Diagnostics.Stopwatch(); // 곡선 이동 테스트 용
        watch.Start(); // 곡선 이동 테스트 용
        position = this.gameObject.transform.position; // 초기 좌표를 목표 좌표에 입력, 대형을 유지하기 위해 필요
        player = GameObject.FindGameObjectWithTag("Player"); // 플레이어 오브젝트 가져오기
        rb = this.gameObject.GetComponent<Rigidbody>(); // 리지드 바디

        // 델리게이트 합연산
        //control += move; // 이동 메소드
        control += p3; // 이동 메소드
        //control += CheckActive; // 활성화 여부 결정 메소드
    }

    float t = 0.0f;
    void Update()
    {
        control(); // 델리게이트 control 실행

        if (t <= 1)
        {
            run(t);
            t += 0.02f;
        }
    }

    // 이동 메소드
    private void move()
    {
        this.gameObject.transform.LookAt(player.transform); // 플레이어를 바라보게 한다.
        this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, position, movementSpeed * Time.deltaTime); // 현재 좌표에서 목표 좌표로 기제된 속도로 이동
    }

    // 활성화 결정 메소드
    // ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ수정 필요ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    private void CheckActive()
    {
        if (this.gameObject.transform.position.z < player.transform.position.z - 10) // 플레이어 뒤로 10만큼 갔을 때 비활성화
        {
            this.gameObject.SetActive(false); // 비활성화
        }
        else if (this.gameObject.transform.position.x < player.transform.position.x - 50) // 플레이어 보다 50 만큼 왼쪽으로 갔을 때 비활성화
        {
            this.gameObject.SetActive(false); // 비활성화
        }
    }

    // 들이받기
    public void MoveToPlayer()
    {
        position = player.transform.position; // 목표 좌표를 플레이어 좌표로 설정
    }

    // 목표 좌표 변경 메소드
    public void ChangeVector3(float x, float y, float z)
    {
        position.x = x;
        position.y = y;
        position.z = z;
    }

    // 플레이어 기준 목표 좌표 변경 메소드
    public void ChangeVector3ToPlayer(float x, float y, float z)
    {
        position.x = x + player.transform.position.x;
        position.y = y + player.transform.position.y;
        position.z = z + player.transform.position.z;
    }

    // 플레이어 기준 회전 메소드
    // 다른 이동 메소드와 같이 실행 되면 이상해지니 주의
    public void RotateAroundPlayer()
    {
        this.gameObject.transform.RotateAround(player.transform.position, Vector3.down, movementSpeed * Time.deltaTime); // 플레이어를 기준으로 회전한다.
    }

    // 기준 회전 메소드
    public void RotateAroundPosition(Vector3 target)
    {
        this.gameObject.transform.RotateAround(target, Vector3.up, movementSpeed * Time.deltaTime);
    }

    // 곡선 이동, 변경 예정, 테스트 중
    void run(float t)
    {
        Vector3 mep = this.gameObject.transform.position; // 해당 오브젝트 현재 좌표 가져오기

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
        Vector3 mep = this.gameObject.transform.position; // 해당 오브젝트 현재 좌표 가져오기
        float x = 0;
        float y = 0;
        float z = 0;

        if (position == this.gameObject.transform.position) // 목표 좌표와 현재 좌표가 같을 때 실행
        {
            x = Random.Range(-20, 20);
            y = Random.Range(-15, 15);
            z = player.transform.position.z - Random.Range(-10, 0);

            ChangeVector3(x, y, z);
        }

        if (mep.z <= player.transform.position.z + 10) // 플레이어와 10만큼 가까워 지면 실행
        {
            z = Random.Range(30, 70);
            ChangeVector3(position.x, position.y, z);
        }
    }

    // 플레이어 왼쪽으로 지나가기
    void p1_1()
    {
        ChangeVector3ToPlayer(-20, -5, -10);
    }

    // 플레이어 오른쪽으로 지나가기
    void p1_2()
    {
        ChangeVector3ToPlayer(20, -5, -10);
    }

    // 플레이어 시점 앞에서 왼쪽으로 이동하기, 플레이어 시야 밖으로
    void p2_1()
    {
        ChangeVector3(-50 + player.transform.position.x, -5 + position.y, 5 + position.z);
    }

    // 플레이어 시점 앞에서 오른쪽으로 이동하기, 플레이어 시야 밖으로
    void p2_2()
    {
        ChangeVector3(50 + player.transform.position.x, -5 + position.y, 5 + position.z);
    }

    // 지그제그
    void p3()
    {
        Vector3 mep = this.gameObject.transform.position; // 해당 오브젝트 현재 좌표 가져오기

        bool change = false; // 목표 좌표값 변경 확인용 

        float x = 0;
        float y = 0;
        float z = 0;

        if (count == 0) // 해당 패턴을 처음 실행할 때
        {
            // 시작 지점으로 이동
            x = 30 + player.transform.position.x;
            y = Random.Range(-10, 10);
            z = 80;

            target = new Vector3(x, y, z); // 다음 이동

            ChangeVector3(x, y, z);

            count++;
        }

            if (mep.x == target.x && mep.z == 80) // 시작 지점일 때
            {
                // 다음 지점 설정
                x = -30;
                y = Random.Range(-20, 20);
                z = 60;

                change = true; // 이동을 위한 true
                target.x = x + player.transform.position.x;
            }
            else if (mep.x == target.x && mep.z == 60) // 1 지점일 때
            {
                // 다음 지점 설정
                x = 30;
                y = Random.Range(-20, 20);
                z = 40;

                change = true; // 이동을 위한 true
                target.x = x + player.transform.position.x;
            }
            else if (mep.x == target.x && mep.z == 40) // 2 지점일 때
            {
                // 다음 지점 설정
                x = -30;
                y = Random.Range(-20, 20);
                z = 20;

                change = true; // 이동을 위한 true
                target.x = x + player.transform.position.x;
            }
            else if (mep.x == target.x && mep.z == 20) // 3 지점일 때
            {
                // 다음 지점 설정
                x = 30;
                y = Random.Range(-20, 20);
                z = 0;

                change = true; // 이동을 위한 true
                target.x = x + player.transform.position.x;
            }

            if (change) // 위 if 문에서 값이 변경 됬을 때
            {
                ChangeVector3ToPlayer(x, y, z - player.transform.position.z); // 지정 좌표로 이동, 플레이어의 z축 이동이 보류됨에 따른 - 연산
            }
        
    }

    // 플레이어에게 이동하다가 플레이어와 20(z)만큼 가까워지면 플레이어 옆으로 지나간다. 얼마나 빗겨갈지는 현재 랜덤
    void p4()
    {
        ChangeVector3(player.transform.position.x, Random.Range(-5, 5), player.transform.position.z);

        if (this.gameObject.transform.position.z <= player.transform.position.z + 20)
        {
            ChangeVector3ToPlayer(Random.Range(-10, 10), Random.Range(-10, 10), -10);
        }
    }

    // 플레이어 앞에서 선회, 사각형을 그린다.
    void p5()
    {
        Vector3 mep = this.gameObject.transform.position; // 해당 오브젝트 현재 좌표 가져오기

        float x = 0;
        float y = 0;
        float z = 0;

        bool change = false; // 목표 좌표값 변경 확인용 

        if (position == this.gameObject.transform.position) // 목표 좌표와 현재 좌표가 같을 때 실행
        {
            // 오브젝트가 플레이어 기준 왼쪽에 있고, 플레이어 기준 z 값이 45 보다 높을 때
            if (mep.x < player.transform.position.x && mep.z > player.transform.position.z + 45)
            {
                // 다음 지점 설정
                x = 30;
                y = Random.Range(-10, 10);
                z = 80;

                change = true; // 이동을 위한 true
            }
            // 오브젝트가 플레이어 기준 오른쪽에 있고, 플레이어 기준 z 값이 45 보다 높을 때
            else if (mep.x > player.transform.position.x && mep.z > player.transform.position.z + 45)
            {
                // 다음 지점 설정
                x = 30;
                y = Random.Range(-10, 10);
                z = 40;

                change = true; // 이동을 위한 true
            }
            // 오브젝트가 플레이어 기준 오른쪽에 있고, 플레이어 기준 z 값이 45 보다 낮을 때
            else if (mep.x > player.transform.position.x && mep.z < player.transform.position.z + 45)
            {
                // 다음 지점 설정
                x = -30;
                y = Random.Range(-10, 10);
                z = 40;

                change = true; // 이동을 위한 true
            }
            // 오브젝트가 플레이어 기준 왼쪽에 있고, 플레이어 기준 z 값이 45 보다 낮을 때
            else if (mep.x < player.transform.position.x && mep.z < player.transform.position.z + 45)
            {
                // 다음 지점 설정
                x = -30;
                y = Random.Range(-10, 10);
                z = 80;

                change = true; // 이동을 위한 true
            }

            if (change) // 위 if 문에서 값이 변경 됬을 때, 즉 목표 좌표가 변경됬을 때
            {
                ChangeVector3ToPlayer(x, y, z - player.transform.position.z); // 지정 좌표로 이동, 플레이어의 z축 이동이 보류됨에 따른 - 연산
            }
        }
    }

    void p6()
    {

    }
}
