using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private Stat pMoveSpeed; // �÷��̾� �̵��ӵ�

    [SerializeField]
    private float pMaxMoveSpeed; // �÷��̾� �̵��ӵ� �ִ밪

    [SerializeField]
    private float pSetMoveSpeed; // �÷��̾� �̵��ӵ� �ʱⰪ

    private Vector3 movementVector;
    private Rigidbody playerRigidbody;
    private float rebound = -1f; // ������ ����� �� �̵� ���� ��ȭ��

    private void Start()
    {
        playerRigidbody = this.GetComponent<Rigidbody>();
        pMoveSpeed.SetDefaultStat(pSetMoveSpeed, pMaxMoveSpeed);
    }
    private void Update()
    {
        Movement();
    }
    // �÷��̾� �̵�
    private void Movement()
    {
        var cameraState = CameraManager.cameraState; // ī�޶� ���°�
        var playerPosition = transform.position;

        // �÷��̾ �̵��ϴ� ����� �ӵ��� ������ ����
        float movementX = Input.GetAxis("XMove"); // A, D
        float movementY = Input.GetAxis("YMove"); // W, S
        if (cameraState == 0) movementVector = new Vector3(movementX, movementY, 0) * pMoveSpeed.currentValue; // ��� ���������� �̵�
        else if (cameraState == 1) movementVector = new Vector3(movementX, 0, movementY) * pMoveSpeed.currentValue; // ž�� ���������� �̵�
        else if (cameraState == 2) movementVector = new Vector3(0, movementY, movementX) * pMoveSpeed.currentValue; // ���̵�� ���������� �̵�

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
