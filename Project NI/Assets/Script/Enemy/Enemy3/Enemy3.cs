using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    public float movementSpeed; // 이동속도
    Transform player; // 플레이어 좌표를 가져오기 위한 오브젝트

    private Vector3 position = Vector3.zero; // 목표 지점 벡터 값, 이 값으로 오브젝트가 움직인다.
    private Rigidbody rb; // 리지드 바디
    private Transform enemyTransform; // 자기 자신의 좌표를 저장

    private void Awake()
    {
        enemyTransform = this.gameObject.transform;
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        position = enemyTransform.position; // 초기 좌표를 목표 좌표에 입력, 대형을 유지하기 위해 필요
    }

    void Update()
    {
        move(); // 델리게이트 control 실행
        MoveToPlayer();
    }

    // 이동 메소드
    private void move()
    {
        enemyTransform.LookAt(player); // 플레이어를 바라보게 한다.
        enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, position, movementSpeed * Time.deltaTime); // 현재 좌표에서 목표 좌표로 기제된 속도로 이동
    }

    // 플레이어 기준 목표 좌표 변경 메소드
    public void ChangeTargetToPlayer(float x, float y, float z)
    {
        position.x = x + player.position.x;
        position.y = y + player.position.y;
        position.z = z + player.position.z;
    }

    // 들이받기
    public void MoveToPlayer()
    {
        position = player.position; // 목표 좌표를 플레이어 좌표로 설정
    }
}