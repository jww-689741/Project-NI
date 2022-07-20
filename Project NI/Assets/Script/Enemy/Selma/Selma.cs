using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selma : MonoBehaviour
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
        SquareMovement();
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

    // �÷��̾� �տ��� ��ȸ, �簢���� �׸���.
    void SquareMovement()
    {
        Vector3 mep = enemyTransform.position; // �ش� ������Ʈ ���� ��ǥ ��������

        float x = 1;
        float z = 0;

        bool change = false; // ��ǥ ��ǥ�� ���� Ȯ�ο� 

        if (mep == position)
        {
            // ������Ʈ�� �÷��̾� ���� ���ʿ� �ְ�, �÷��̾� ���� z ���� 45 ���� ���� ��
            if (mep.x <= player.position.x && mep.z >= player.position.z + 45)
            {
                // ���� ���� ����
                x = 1;
                z = 80;
            }
            // ������Ʈ�� �÷��̾� ���� �����ʿ� �ְ�, �÷��̾� ���� z ���� 45 ���� ���� ��
            else if (mep.x >= player.position.x && mep.z >= player.position.z + 45)
            {
                // ���� ���� ����
                x = -1;
                z = 40;
            }
            // ������Ʈ�� �÷��̾� ���� �����ʿ� �ְ�, �÷��̾� ���� z ���� 45 ���� ���� ��
            else if (mep.x >= player.position.x && mep.z <= player.position.z + 45)
            {
                // ���� ���� ����
                x = 1;
                z = 40;
            }
            // ������Ʈ�� �÷��̾� ���� ���ʿ� �ְ�, �÷��̾� ���� z ���� 45 ���� ���� ��
            else if (mep.x <= player.position.x && mep.z <= player.position.z + 45)
            {
                // ���� ���� ����
                x = -1;
                z = 80;
            }

            change = true; // �̵��� ���� true
        }

        if (change) // �� if ������ ���� ���� ?�� ��, �� ��ǥ ��ǥ�� ����?�� ��
        {
            x *= 30 * DistanceCalculation(mep.x);
            ChangeTargetToPlayer(x, Random.Range(-20, 20), z - player.position.z); // ���� ��ǥ�� �̵�, �÷��̾��� z�� �̵��� �����ʿ� ���� - ����
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
