using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billy : MonoBehaviour
{
    private Vector3 position = Vector3.zero; // ��ǥ ���� ���� ��, �� ������ ������Ʈ�� �����δ�.
    private Transform player; // �÷��̾� ��ǥ�� �������� ���� ������Ʈ
    private Transform enemyTransform; // �ڱ� �ڽ��� ��ǥ�� ����
    private float movementSpeed; // �̵��ӵ�
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
        RoundTrip();
    }

    // �̵� �޼ҵ�
    private void move()
    {
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
