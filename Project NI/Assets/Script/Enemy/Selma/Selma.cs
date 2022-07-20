using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selma : MonoBehaviour
{
    private Vector3 position = Vector3.zero; // 목표 지점 벡터 값, 이 값으로 오브젝트가 움직인다.
    private Transform player; // 플레이어 좌표를 가져오기 위한 오브젝트
    private Transform enemyTransform; // 자기 자신의 좌표를 저장
    private float movementSpeed; // 이동속도
    private bool direction;

    private void Awake()
    {
        enemyTransform = this.gameObject.transform;
    }

    void Start()
    {
        var status = GetComponent<SaraStatusManager>();
        player = GameObject.FindWithTag("Player").transform;
        position = enemyTransform.position;
        movementSpeed = status.GetMoveSpeed();
        direction = Random.value > 0.5f;
    }

    void Update()
    {
        move();
        SquareMovement();
    }

    // 이동 메소드
    private void move()
    {
        enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, position, movementSpeed * Time.deltaTime); // 현재 좌표에서 목표 좌표로 기제된 속도로 이동
    }

    // 플레이어 기준 목표 좌표 변경 메소드
    public void ChangeTargetToPlayer(float x, float y, float z)
    {
        position.x = x + player.position.x;
        position.y = y + player.position.y;
        position.z = z + player.position.z;
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

        if (change) // 위 if 문에서 값이 변경 ?될 때, 즉 목표 좌표가 변경?될 때
        {
            x *= 30 * DistanceCalculation(mep.x);
            ChangeTargetToPlayer(x, Random.Range(-20, 20), z - player.position.z); // 지정 좌표로 이동, 플레이어의 z축 이동이 보류됨에 따른 - 연산
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
}
