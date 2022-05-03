using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 각 개체당 개별 이동 클레스
public class EnemyMove : MonoBehaviour
{
    public float repeaterInterval;
    public float height;
    public float movementSpeed; // 이동속도
    Transform player; // 플레이어 좌표를 가져오기 위한 오브젝트

    private Vector3 startPoint = Vector3.zero; // 곡선이동 초기 좌표
    private Vector3[] shotPoint; // 적 공격용 좌표 배열
    private Vector3 position = Vector3.zero; // 목표 지점 벡터 값, 이 값으로 오브젝트가 움직인다.
    private Rigidbody rb; // 리지드 바디
    private Transform enemyTransform; // 자기 자신의 좌표를 저장
    private bool confirmDoneFlag = false; // 곡선이동 초기 좌표 지정 확인
    private delegate void Control(); // 이동용 델리게이트
    Control control; // 선언

    private void Awake()
    {
        enemyTransform = this.gameObject.transform;
    }

    void Start()
    {
        player = GameObject.Find("SPTest").transform;
        shotPoint = new Vector3[2];
        position = enemyTransform.position; // 초기 좌표를 목표 좌표에 입력, 대형을 유지하기 위해 필요
        rb = GetComponent<Rigidbody>(); // 리지드 바디

        // 델리게이트 합연산
        control += move; // 이동 메소드
        //control += StepMovement;
        //control += RoundTrip;
        //control += SquareMovement;
    }

    [Range(0f, 1f)] // 0 ~ 1 범위에서만 조작되게 제한하기 위한 설정
    public float t = 0f;
    public float timer = 0;

    void Update()
    {
        //control(); // 델리게이트 control 실행

        /*
        if (timer > 10)
        {
            StartCoroutine("Repeater", "DirectBullet");
            timer = 0;
        }
        timer += Time.deltaTime;

        if (t <= 1)
        {
            run(t);
            t += Time.deltaTime * 0.6f;
        }
        else
        {
            run(1);
            t = 0.0f;
            confirmDoneFlag = false;
        }
        */
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
        if (!confirmDoneFlag)
        {
            startPoint = enemyTransform.position; // 해당 오브젝트 현재 좌표 가져오기
            confirmDoneFlag = true;
        }

        Vector3 startHeightPos = startPoint + new Vector3(0, 1, 0) * height;
        Vector3 endHeightPos = position + player.position + new Vector3(0, 1, 0) * height;

        Vector3 a = Vector3.Lerp(startPoint, startHeightPos, t);
        Vector3 b = Vector3.Lerp(startHeightPos, endHeightPos, t);
        Vector3 c = Vector3.Lerp(endHeightPos, position, t);

        Vector3 d = Vector3.Lerp(a, b, t);
        Vector3 e = Vector3.Lerp(b, c, t);

        Vector3 f = Vector3.Lerp(d, e, t);

        transform.position = f;
    }

    // 지속 좌표 변경 메소드
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
            ChangeTarget(-50 + player.position.x, -5 + player.position.y, 5 + player.position.z); // 플레이어 시점 앞에서 왼쪽으로 이동하기, 플레이어 시야 밖으로
        }
        else
        {
            ChangeTarget(50 + player.position.x, -5 + player.position.y, 5 + player.position.z); // 플레이어 시점 앞에서 오른쪽으로 이동하기, 플레이어 시야 밖으로
        }
    }

    // 지그제그
    void StepMovement()
    {
        Vector3 mep = enemyTransform.position; // 해당 오브젝트 현재 좌표 가져오기

        bool change = false; // 목표 좌표값 변경 확인용 

        float x;
        float z = 0;

        if (mep == position)
        {
            if (mep.z >= 80)
            {
                z = 80;

                if (mep.z == 80)
                {
                    z = 60;
                }
                change = true;
            }
            else if (mep.z >= 60)
            {
                z = 60;

                if (mep.z == 60)
                {
                    z = 40;
                }
                change = true;
            }
            else if (mep.z >= 40)
            {
                z = 40;

                if (mep.z == 40)
                {
                    z = 20;
                }
                change = true;
            }
            else if (mep.z >= 20)
            {
                z = 20;

                if (mep.z == 20)
                {
                    z = 0;
                }
                change = true;
            }
        }

        if (change) // 위 if 문에서 값이 변경 될 때
        {
            x = 30 * DistanceCalculation(mep.x);
            ChangeTargetToPlayer(x, Random.Range(-20, 20), z - player.position.z); // 지정 좌표로 이동
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

        float x = 1;
        float z = 0;

        bool change = false; // 목표 좌표값 변경 확인용 

        if (mep == position)
        {
            // 오브젝트가 플레이어 기준 왼쪽에 있고, 플레이어 기준 z 값이 45 보다 높을 때
            if (mep.x <= player.position.x && mep.z >= player.position.z + 45)
            {
                // 다음 지점 설정
                x = 1;
                z = 80;
            }
            // 오브젝트가 플레이어 기준 오른쪽에 있고, 플레이어 기준 z 값이 45 보다 높을 때
            else if (mep.x >= player.position.x && mep.z >= player.position.z + 45)
            {
                // 다음 지점 설정
                x = -1;
                z = 40;
            }
            // 오브젝트가 플레이어 기준 오른쪽에 있고, 플레이어 기준 z 값이 45 보다 낮을 때
            else if (mep.x >= player.position.x && mep.z <= player.position.z + 45)
            {
                // 다음 지점 설정
                x = 1;
                z = 40;
            }
            // 오브젝트가 플레이어 기준 왼쪽에 있고, 플레이어 기준 z 값이 45 보다 낮을 때
            else if (mep.x <= player.position.x && mep.z <= player.position.z + 45)
            {
                // 다음 지점 설정
                x = -1;
                z = 80;
            }

            change = true; // 이동을 위한 true
        }

        if (change) // 위 if 문에서 값이 변경 될 때, 즉 목표 좌표가 변경될 때
        {
            x *= 30 * DistanceCalculation(mep.x);
            ChangeTargetToPlayer(x, Random.Range(-20, 20), z - player.position.z); // 지정 좌표로 이동, 플레이어의 z축 이동이 보류됨에 따른 - 연산
        }
    }

    // 왕복 이동
    void RoundTrip()
    {
        if (enemyTransform.position == position) // 현재 좌표가 목표 좌표와 같다면 실행
        {
            ChangeTargetToPlayer(0, 0, 0); // 플레이어 좌표로 이동
        }

        if (enemyTransform.position.z < player.position.z + 40) // 플레이어와 15만큼 가까워지면
        {
            ChangeTargetToPlayer(Random.Range(-50, 50), Random.Range(-20, 20), Random.Range(100, 160)); // -30~30, -20~20, 60~100의 좌표로 이동한다.
        }
    }

    // 
    private int DistanceCalculation(float number)
    {
        if (number >= 0)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }

    IEnumerator Repeater(string name)
    {
        SetBullet(ObjectManager.instance.GetBullet(name), name); // 탄환 발사
        yield return new WaitForSeconds(repeaterInterval); // 연사시간만큼 대기
    }

    // 탄환 오브젝트 위치 지정, 회전각도 설정, 오브젝트 활성화, 실제 발사 로직 작동
    private void SetBullet(GameObject bullet, string name)
    {
        if (bullet == null) return; // 받아올 탄환이 없을 경우 반환

        Vector3 bulletPosition = new Vector3(this.transform.position.x, this.transform.position.y, (this.transform.position.z - 1.5f));

        shotPoint[0] = player.position; // 목표지점, 플레이어
        shotPoint[1] = bulletPosition; // 시작지점, 적

        bullet.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, (this.transform.position.z - 1.5f)); // 위치 지정
        bullet.transform.rotation = this.transform.rotation; // 회전각 지정
        bullet.SetActive(true); // 활성화

        if (name == "DirectBullet")
        {
            bullet.GetComponent<DirectBulletStatusManager>().StartCoroutine("Shot", shotPoint); // 탄환 동작 로직 코루틴 시작
        }
        else if (name == "HowitzerBullet")
        {
            bullet.GetComponent<HowitzerBulletStatusManager>().StartCoroutine("Shot", shotPoint); // 탄환 동작 로직 코루틴 시작
        }
        else if (name == "Buckshot")
        {
            for (int i = 0; i < 5; i++)
            {
                bullet.transform.GetChild(i).transform.position = bullet.transform.position;  //자식 오브젝트 부모와 동일한 위치로 이동
            }
            bullet.GetComponent<BuckShot>().StartCoroutine("Shot", shotPoint); // 탄환 동작 로직 코루틴 시작
        }
    }
}