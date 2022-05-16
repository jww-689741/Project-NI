using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private Stat pMoveSpeed; // 플레이어 이동속도

    [SerializeField]
    private float pMaxMoveSpeed; // 플레이어 이동속도 최대값

    [SerializeField]
    private float pSetMoveSpeed; // 플레이어 이동속도 초기값

    private Vector3 movementVector;
    private Rigidbody playerRigidbody;
    private float rebound = -1f; // 범위를 벗어났을 때 이동 방향 변화량

    private void Start()
    {
        playerRigidbody = this.GetComponent<Rigidbody>();
        pMoveSpeed.SetDefaultStat(pSetMoveSpeed, pMaxMoveSpeed);
    }
    private void Update()
    {
        Movement();
    }
    // 플레이어 이동
    private void Movement()
    {
        var cameraState = CameraManager.cameraState; // 카메라 상태값
        var playerPosition = transform.position;

        // 플레이어가 이동하는 방향과 속도를 삽입한 벡터
        float movementX = Input.GetAxis("XMove"); // A, D
        float movementY = Input.GetAxis("YMove"); // W, S
        if (cameraState == 0) movementVector = new Vector3(movementX, movementY, 0) * pMoveSpeed.currentValue; // 백뷰 시점에서의 이동
        else if (cameraState == 1) movementVector = new Vector3(movementX, 0, movementY) * pMoveSpeed.currentValue; // 탑뷰 시점에서의 이동
        else if (cameraState == 2) movementVector = new Vector3(0, movementY, movementX) * pMoveSpeed.currentValue; // 사이드뷰 시점에서의 이동

        if (playerPosition.y >= 20)
        {
            playerRigidbody.velocity = movementVector;
            Debug.Log(movementVector);
            rebound = -1f;
        }
        else if (playerPosition.y < 20)
        {
            if (cameraState == 0) playerRigidbody.velocity = new Vector3(movementX, rebound, 0) * pMoveSpeed.currentValue;
            else if (cameraState == 2) playerRigidbody.velocity = new Vector3(0, rebound, movementX) * pMoveSpeed.currentValue;
            rebound += 0.01f;
        }
    }
}
