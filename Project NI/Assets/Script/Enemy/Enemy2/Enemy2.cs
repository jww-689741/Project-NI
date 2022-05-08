using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
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
        RoundTrip();
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

    // �պ� �̵�
    void RoundTrip()
    {
        if (enemyTransform.position == position) // ���� ��ǥ�� ��ǥ ��ǥ�� ���ٸ� ����
        {
            ChangeTargetToPlayer(0, 0, 0); // �÷��̾� ��ǥ�� �̵�
        }

        if (enemyTransform.position.z < player.position.z + 40) // �÷��̾�� 15��ŭ ���������
        {
            ChangeTargetToPlayer(Random.Range(-50, 50), Random.Range(-20, 20), Random.Range(100, 160)); // -30~30, -20~20, 60~100�� ��ǥ�� �̵��Ѵ�.
        }
    }
}
