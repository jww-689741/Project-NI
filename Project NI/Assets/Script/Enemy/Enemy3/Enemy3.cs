using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    public float movementSpeed; // �̵��ӵ�
    Transform player; // �÷��̾� ��ǥ�� �������� ���� ������Ʈ

    private Vector3 position = Vector3.zero; // ��ǥ ���� ���� ��, �� ������ ������Ʈ�� �����δ�.
    private Rigidbody rb; // ������ �ٵ�
    private Transform enemyTransform; // �ڱ� �ڽ��� ��ǥ�� ����

    private void Awake()
    {
        enemyTransform = this.gameObject.transform;
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        position = enemyTransform.position; // �ʱ� ��ǥ�� ��ǥ ��ǥ�� �Է�, ������ �����ϱ� ���� �ʿ�
    }

    void Update()
    {
        move(); // ��������Ʈ control ����
        MoveToPlayer();
    }

    // �̵� �޼ҵ�
    private void move()
    {
        enemyTransform.LookAt(player); // �÷��̾ �ٶ󺸰� �Ѵ�.
        enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, position, movementSpeed * Time.deltaTime); // ���� ��ǥ���� ��ǥ ��ǥ�� ������ �ӵ��� �̵�
    }

    // �÷��̾� ���� ��ǥ ��ǥ ���� �޼ҵ�
    public void ChangeTargetToPlayer(float x, float y, float z)
    {
        position.x = x + player.position.x;
        position.y = y + player.position.y;
        position.z = z + player.position.z;
    }

    // ���̹ޱ�
    public void MoveToPlayer()
    {
        position = player.position; // ��ǥ ��ǥ�� �÷��̾� ��ǥ�� ����
    }
}