using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Irving : MonoBehaviour
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
        StepMovement();
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

        if (change) // 위 if 문에서 값이 변경 ?될 때
        {
            x = 30 * DistanceCalculation(mep.x);
            ChangeTargetToPlayer(x, Random.Range(-20, 20), z - player.position.z); // 지정 좌표로 이동
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
